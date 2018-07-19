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
        [HttpGet("{sqPlano}")]
        [Authorize("Bearer")]
        public IActionResult Get(int sqPlano)
        {
            try
            {
                //TODO: Jogar pra proxy

                var contribuicoes = new ContribuicaoProxy().BuscarPorPlanoContratoTrabalho(SqContratoTrabalho, sqPlano);

                // Data de referência do salário de participação
                var dataReferencia = contribuicoes.First().DT_REFERENCIA;

                // Busca o salário de participação
                var salarioContribuicao = new SalarioContribuicaoProxy().BuscarPorContratoTrabalhoPlanoReferencia(SqContratoTrabalho, sqPlano, dataReferencia);
                var salarioParticipacao = salarioContribuicao.VL_BASE_FUNDACAO;

                return Json(new
                {
                    dataReferencia,
                    salarioParticipacao
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{sqPlano}/passo2")]
        [Authorize("Bearer")]
        public IActionResult GetPasso2(int sqPlano)
        {
            try
            {
                //TODO: Jogar pra proxy

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

        [HttpPost("{sqPlano}/simular")]
        [Authorize("Bearer")]
        public IActionResult GetSimulacao(int sqPlano, [FromBody] dynamic dados)
        {
            try
            {
                //TODO: Jogar pra proxy

                int idadeAposentadoria = Convert.ToInt32(dados.idadeAposentadoria);
                decimal contribBasica = Convert.ToDecimal(dados.contribBasica);
                decimal contribFacultativa = Convert.ToDecimal(dados.contribFacultativa);

                var saldo = new SaldoProxy().BuscarSaldoCD(DateTime.Now, SqContratoTrabalho, sqPlano, CdPessoa).Total;
                var taxaJuros = new FatorValidadeProxy().BuscarUltimo().VL_TX_JUROS;

                var dataAtual = DateTime.Now.PrimeiroDiaDoMes();
                var dataNascimento = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa).DT_NASCIMENTO.Value;

                var dataAposentadoria = dataNascimento.AddYears(idadeAposentadoria);
                var data = DateTime.Compare(dataAtual, dataAposentadoria) > 0 ? dataAposentadoria : dataAtual;
                var contribBruta = contribBasica * 2 + contribFacultativa;
                var taxaMensal = BuscarTaxaMensal(taxaJuros);
                var meses = new Intervalo(dataAposentadoria, dataAtual).Meses;

                var valorFuturo = (decimal)saldo * (decimal)Math.Pow((double)(1 + taxaMensal), meses) + (decimal)contribBruta * ((decimal)Math.Pow((double)(1 + taxaMensal), meses) - 1) / (decimal)taxaMensal;

                return Json(new
                {
                    valorFuturo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public decimal BuscarTaxaMensal(decimal taxaAnual) {
            var mensal = 1M / 12M;
            var valor = taxaAnual / 100 + 1;
            return (decimal)(Math.Pow((double)valor, (double)mensal) - 1) * 100;
        }
    }
}