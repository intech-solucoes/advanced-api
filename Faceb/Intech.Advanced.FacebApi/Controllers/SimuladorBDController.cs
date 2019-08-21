#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Intech.Advanced.BaseApi;
using Intech.Advanced.Negocio.Proxy;
using Intech.Lib.Util.Date;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route(RotasApi.SimuladorBD)]
    [ApiController]
    public class SimuladorBDController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            try
            {
                //TODO: Jogar pra proxy

                var sqPlano = 1;

                int idadeParticipante = CalcularIdadeParticipante();
                int idadeMinima = CalcularIdadeMinima(idadeParticipante);
                decimal SRC = CalcularSRC(sqPlano);
                decimal inssHipotetico = CalcularINSSHipotetico(sqPlano);
                int carencia = CalcularCarencia(sqPlano);

                return Json(new
                {
                    idadeMinima,
                    SRC,
                    inssHipotetico,
                    carencia
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("simular")]
        [Authorize("Bearer")]
        public IActionResult GetSimulacao()
        {
            try
            {
                var sqPlano = 1;

                int idadeParticipante = CalcularIdadeParticipante();
                int idadeMinima = CalcularIdadeMinima(idadeParticipante);
                decimal SRC = CalcularSRC(sqPlano);
                decimal inssHipotetico = CalcularINSSHipotetico(sqPlano);
                int carencia = CalcularCarencia(sqPlano);

                var valor1 = (SRC - inssHipotetico) * carencia / 15;
                var valor2 = (SRC * 0.25M) * carencia / 15;

                var valorSuplementacao = Math.Max(valor1, valor2);
                var dataNascimento = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa).DT_NASCIMENTO;
                var dataAposentadoriaPorIdade = dataNascimento.Value.AddYears(idadeMinima);

                var plano = new PlanoVinculadoProxy().BuscarPorContratoTrabalhoPlano(SqContratoTrabalho, sqPlano);
                var dataAposentadoriaPorPlano = plano.DT_INSC_PLANO.AddYears(15);

                var tempoServico = new TempoServicoProxy().BuscarPorCdPessoa(CdPessoa);

                var tempoTotal = new Intervalo(new CalculoAnosMesesDiasAlgoritmo2());

                foreach(var tempo in tempoServico)
                {
                    DateTime fim;

                    if (tempo.DT_TERM_ATIVIDADE.HasValue)
                        fim = tempo.DT_TERM_ATIVIDADE.Value;
                    else
                        fim = DateTime.Today;

                    var intervaloTrabalho = new Intervalo(fim, tempo.DT_INIC_ATIVIDADE.Value, new CalculoAnosMesesDiasAlgoritmo2());

                    tempoTotal.Adiciona(intervaloTrabalho);
                }

                var dadosPessoais = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa);
                var fatorSexo = dadosPessoais.IR_SEXO == null || dadosPessoais.IR_SEXO == "M" ? 35 : 30;

                var dataAposentadoriaPorTempoTrabalho
                    = tempoTotal.Anos < fatorSexo
                    ? tempoServico.First().DT_INIC_ATIVIDADE.Value.AddYears(fatorSexo)
                    : tempoServico.First().DT_INIC_ATIVIDADE.Value.AddDays(tempoTotal.Dias);

                var listaDatas = new List<DateTime>
                {
                    dataAposentadoriaPorIdade,
                    dataAposentadoriaPorPlano,
                    dataAposentadoriaPorTempoTrabalho
                };

                var dataAposentadoria = listaDatas.Max(x => x);

                var dataReferencia = DateTime.Now;

                return Json(new
                {
                    valorSuplementacao,
                    dataAposentadoria,
                    dataReferencia
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Métodos Privados

        int CalcularIdadeParticipante()
        {
            var dadosPessoais = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa);
            var idadeParticipante = new Intervalo(DateTime.Now, dadosPessoais.DT_NASCIMENTO.Value, new CalculoAnosMesesDiasAlgoritmo2()).Anos;
            return idadeParticipante;
        }

        int CalcularIdadeMinima(int idadeParticipante)
        {
            var idadeMinima = 55;

            if (idadeParticipante > idadeMinima)
                idadeMinima = idadeParticipante;

            return idadeMinima;
        }

        decimal CalcularSRC(int sqPlano)
        {
            var salarioContrib = new SalarioContribuicaoProxy().BuscarUltimoPorContratoTrabalhoPlano(SqContratoTrabalho, sqPlano);
            var SRC = salarioContrib.VL_BASE_FUNDACAO.Value;
            return SRC;
        }

        decimal CalcularINSSHipotetico(int sqPlano)
        {
            var indiceProxy = new IndiceProxy();

            var dtFim = DateTime.Now.AddMonths(-1).DefinirDia(1);
            var dtIni = dtFim.AddMonths(-36);

            var indiceINPC = indiceProxy.BuscarPorCdIndicePeriodo("INPC", dtIni, dtFim);

            var fatores = new List<KeyValuePair<DateTime, decimal>>();

            var fatorAnterior = 1M;

            foreach (var indice in indiceINPC)
            {
                var fator = (fatorAnterior * (1 + indice.VL_INDICE.Value / 100)).Arredonda(6);
                fatores.Add(new KeyValuePair<DateTime, decimal>(indice.DT_INIC_VALIDADE, fator));
                fatorAnterior = fator;
            }

            var indiceTETOPREV = indiceProxy.BuscarPorCdIndice("TETOPREV");
            var salarios = new SalarioContribuicaoProxy().BuscarPorContratoTrabalhoPlanoPeriodo(SqContratoTrabalho, sqPlano, dtIni, dtFim);
            var salariosCorrigidos = new List<KeyValuePair<DateTime, decimal>>();

            foreach (var salario in salarios)
            {
                var dtReferencia = salario.DT_REFERENCIA;
                var teto = indiceTETOPREV.FirstOrDefault(x => x.DT_INIC_VALIDADE <= dtReferencia);

                if (teto == null)
                    throw new Exception($"Índice TETOPREV não encontrado para a referência {dtReferencia.ToString("dd/MM/yyyy")}");

                if (!salario.VL_BASE_PREVIDENCIA.HasValue)
                    throw new Exception($"Salário de contribuição não encontrado na data de {salario.DT_REFERENCIA}");

                var valor = Math.Min(salario.VL_BASE_PREVIDENCIA.Value, teto.VL_INDICE.Value);
                var fator = fatores.First(x => x.Key <= dtReferencia);

                valor = valor * fator.Value;

                salariosCorrigidos.Add(new KeyValuePair<DateTime, decimal>(dtReferencia, valor));
            }

            var inssHipotetico = salariosCorrigidos.Sum(x => x.Value) / 36;

            inssHipotetico = Math.Min(inssHipotetico, indiceTETOPREV.First().VL_INDICE.Value);
            return inssHipotetico;
        }

        int CalcularCarencia(int sqPlano)
        {
            var plano = new PlanoVinculadoProxy().BuscarPorContratoTrabalhoPlano(SqContratoTrabalho, sqPlano);
            var idadePlano = new Intervalo(DateTime.Now, plano.DT_INSC_PLANO, new CalculoAnosMesesDiasAlgoritmo2()).Anos;
            var carencia = Math.Min(idadePlano, 15);
            return carencia;
        }

        #endregion
    }
}