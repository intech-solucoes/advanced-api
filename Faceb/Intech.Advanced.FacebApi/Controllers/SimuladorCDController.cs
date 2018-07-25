#region Usings
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Intech.Advanced.BaseApi;
using Intech.Advanced.Negocio.Proxy;
using Intech.Lib.Util.Date;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route(RotasApi.SimuladorCD)]
    [ApiController]
    public class SimuladorCDController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            try
            {
                //TODO: Jogar pra proxy

                var sqPlano = 3;

                var contribuicoes = new ContribuicaoProxy().BuscarPorPlanoContratoTrabalho(SqContratoTrabalho, sqPlano);

                // Data de referência do salário de participação
                var dataReferencia = contribuicoes.First().DT_REFERENCIA;

                // Busca o salário de participação
                var salarioContribuicao = new SalarioContribuicaoProxy().BuscarPorContratoTrabalhoPlanoReferencia(SqContratoTrabalho, sqPlano, dataReferencia);
                var salarioParticipacao = salarioContribuicao.VL_BASE_FUNDACAO;

                // Busca o percentual de contribuição
                var percentualContribuicao = new HistManutContribuicaoProxy().BuscarUltimoPorContratoTrabalho(SqContratoTrabalho);
                var percentual = percentualContribuicao.VL_COEF_TAXA;

                return Json(new
                {
                    dataReferencia,
                    salarioParticipacao,
                    percentual
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("passo2")]
        [Authorize("Bearer")]
        public IActionResult GetPasso2()
        {
            try
            {
                //TODO: Jogar pra proxy

                var sqPlano = 3;

                var saldo = new SaldoProxy().BuscarSaldoCD(DateTime.Now, SqContratoTrabalho, sqPlano, CdPessoa).Total;
                var taxaJuros = new FatorValidadeProxy().BuscarUltimo().VL_TX_JUROS;

                return Json(new
                {
                    saldo,
                    taxaJuros
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("simular")]
        [Authorize("Bearer")]
        public IActionResult GetSimulacao([FromBody] dynamic dados)
        {
            try
            {
                //TODO: Jogar pra proxy

                var sqPlano = 3;

                int idadeAposentadoria = Convert.ToInt32(dados.idadeAposentadoria);
                decimal contribBasica = Convert.ToDecimal(dados.contribBasica, new CultureInfo("pt-BR"));
                decimal contribFacultativa = Convert.ToDecimal(dados.contribFacultativa, new CultureInfo("pt-BR"));

                var saldo = new SaldoProxy().BuscarSaldoCD(DateTime.Now, SqContratoTrabalho, sqPlano, CdPessoa).Total;
                var taxaJuros = new FatorValidadeProxy().BuscarUltimo().VL_TX_JUROS;

                var dataAtual = DateTime.Now.PrimeiroDiaDoMes();
                var dataNascimento = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa).DT_NASCIMENTO.Value;

                var dataAposentadoria = dataNascimento.AddYears(idadeAposentadoria);
                var data = DateTime.Compare(dataAtual, dataAposentadoria) > 0 ? dataAposentadoria : dataAtual;
                var contribBruta = contribBasica * 2 + contribFacultativa;
                var taxaMensal = BuscarTaxaMensal(taxaJuros);
                var meses = new Intervalo(dataAposentadoria, dataAtual).Meses;

                // Saldo futuro
                var valorFuturo = (decimal)saldo * (decimal)Math.Pow((double)(1 + taxaMensal), meses) + (decimal)contribBruta * ((decimal)Math.Pow((double)(1 + taxaMensal), meses) - 1) / (decimal)taxaMensal;

                // Valor do saque
                var valorSaque = dados.saque == "N" ? 0 : valorFuturo / 100 * Convert.ToInt32(dados.saque);

                // Dependentes
                DateTime dataNascDependente;
                int? idadeDependente = null;
                var dependenteProxy = new DependenteProxy();
                var dependenteVitalicio = dependenteProxy.BuscarDependentePorContratoTrabalhoDtValidadeTipo(SqContratoTrabalho, "V", dataAposentadoria);

                if (dependenteVitalicio != null)
                    dataNascDependente = dependenteVitalicio.DT_NASCIMENTO;
                else
                {
                    var dependenteTemporario = dependenteProxy.BuscarDependentePorContratoTrabalhoDtValidadeTipo(SqContratoTrabalho, "T", dataAposentadoria);
                    dataNascDependente = dependenteTemporario.DT_NASCIMENTO;
                }

                if(dataNascDependente != null)
                    idadeDependente = new Intervalo(dataAposentadoria, dataNascDependente).Anos;
                
                // Fator atuarial
                var fatorAtuarialProxy = new FatorAtuarialMortalidadeProxy();
                var axiPar = fatorAtuarialProxy.BuscarPorIdade(idadeAposentadoria).VL_FATOR_A.Value;

                var axiDep = 0M;
                var axyi = 0M;
                if (idadeDependente != null)
                {
                    axiDep = fatorAtuarialProxy.BuscarPorIdade(idadeDependente.Value).VL_FATOR_A.Value;
                    var fator = fatorAtuarialProxy.BuscarPorIdadePartIdadeDep(idadeAposentadoria, idadeDependente.Value);
                    axyi = fator.VL_FATOR_A.Value;
                }

                var fatorAtuarialPensaoMorte = 13 * axyi;
                var fatorAtuarialSemPensaoMorte = 13 * axiPar;

                // Renda por prazos indeterminados
                var rendaPrazoIndeterminadoPensaoMorte = (valorFuturo - valorSaque) / fatorAtuarialPensaoMorte;
                var rendaPrazoIndeterminadoSemPensaoMorte = (valorFuturo - valorSaque) / fatorAtuarialSemPensaoMorte;

                // Renda por prazo certo
                var listaPrazos = new List<KeyValuePair<int, decimal>>();

                for (int prazo = 15; prazo <= 25; prazo++)
                {
                    decimal valor = (valorFuturo - valorSaque) / (prazo * 13);
                    listaPrazos.Add(new KeyValuePair<int, decimal>(prazo, valor));
                }

                // Renda por percentual do saldo de contas
                var listaSaldoPercentuais = new List<KeyValuePair<string, decimal>>();

                for (decimal percentual = 0.5M; percentual <= 2.0M; percentual += 0.5M)
                {
                    decimal valor = (valorFuturo - valorSaque) * percentual / 100;
                    listaSaldoPercentuais.Add(new KeyValuePair<string, decimal>(percentual.ToString("N1").Replace(".", ","), valor));
                }

                return Json(new
                {
                    valorFuturo,
                    dataAposentadoria,
                    valorSaque,
                    idadeDependente,
                    fatorAtuarialPensaoMorte,
                    fatorAtuarialSemPensaoMorte,
                    rendaPrazoIndeterminadoPensaoMorte,
                    rendaPrazoIndeterminadoSemPensaoMorte,
                    listaPrazos,
                    listaSaldoPercentuais
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        decimal BuscarTaxaMensal(decimal taxaAnual)
        {
            var mensal = 1M / 12M;
            var valor = taxaAnual / 100 + 1;
            return (decimal)(Math.Pow((double)valor, (double)mensal) - 1) * 100;
        }
    }
}