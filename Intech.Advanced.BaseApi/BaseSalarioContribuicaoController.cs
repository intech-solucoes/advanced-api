#region Usings
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization; 
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BaseSalarioContribuicaoController : BaseController
    {
        [HttpGet("porPlanoReferencia/{SQ_PLANO_PREVIDENCIAL}/{dataReferencia}")]
        [Authorize("Bearer")]
        public IActionResult PorContratoTrabalhoPlanoReferencia(int SQ_PLANO_PREVIDENCIAL, string dataReferencia)
        {
            try
            {
                var dtReferencia = DateTime.ParseExact(dataReferencia, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                return Json(new SalarioContribuicaoProxy().BuscarPorContratoTrabalhoPlanoReferencia(SqContratoTrabalho, SQ_PLANO_PREVIDENCIAL, dtReferencia));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porPlanoPeriodo/{SQ_PLANO_PREVIDENCIAL}/{dtIni}/{dtFim}")]
        [Authorize("Bearer")]
        public IActionResult PorContratoTrabalhoPlanoPeriodo(int SQ_PLANO_PREVIDENCIAL, string dtIni, string dtFim)
        {
            try
            {
                var dataInicio = DateTime.ParseExact(dtIni, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                var dataFim = DateTime.ParseExact(dtFim, "dd.MM.yyyy", new CultureInfo("pt-BR"));

                return Json(new SalarioContribuicaoProxy().BuscarPorContratoTrabalhoPlanoPeriodo(SqContratoTrabalho, SQ_PLANO_PREVIDENCIAL, dataInicio, dataFim));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ultimoPorPlano/{SQ_PLANO_PREVIDENCIAL}")]
        [Authorize("Bearer")]
        public IActionResult UltimoPorContratoTrabalhoPlano(int SQ_PLANO_PREVIDENCIAL)
        {
            try
            {
                return Json(new SalarioContribuicaoProxy().BuscarUltimoPorContratoTrabalhoPlano(SqContratoTrabalho, SQ_PLANO_PREVIDENCIAL));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
