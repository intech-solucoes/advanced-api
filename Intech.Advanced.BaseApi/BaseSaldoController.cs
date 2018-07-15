#region Usings
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization; 
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BaseSaldoController : BaseController
    {
        [HttpGet("individualPorPlanoIrTipoDataReferencia/{SQ_PLANO_PREVIDENCIAL}/{IR_TIPO}/{DT_REFERENCIA}")]
        [Authorize("Bearer")]
        public IActionResult GetIndividualPorPlanoDataReferenciaPessoa(int SQ_PLANO_PREVIDENCIAL, string IR_TIPO, string DT_REFERENCIA)
        {
            try
            {
                var dtReferencia = DateTime.ParseExact(DT_REFERENCIA, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                return Json(new SaldoProxy().BuscarIndividualPorPlanoDataReferenciaPessoa(dtReferencia, SQ_PLANO_PREVIDENCIAL, CdPessoa, IR_TIPO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("saldoBD/{SQ_PLANO_PREVIDENCIAL}/{DT_REFERENCIA}")]
        [Authorize("Bearer")]
        public IActionResult GetSaldoBD(int SQ_PLANO_PREVIDENCIAL, string DT_REFERENCIA)
        {
            try
            {
                var dtReferencia = DateTime.ParseExact(DT_REFERENCIA, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                return Json((new SaldoProxy().BuscarSaldoBD(dtReferencia, SqContratoTrabalho, SQ_PLANO_PREVIDENCIAL, CdPessoa)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("brutoPorPlanoDataReferencia/{SQ_PLANO_PREVIDENCIAL}/{DT_REFERENCIA}")]
        [Authorize("Bearer")]
        public IActionResult GetIndividualPorPlanoDataReferenciaPessoa(int SQ_PLANO_PREVIDENCIAL, string DT_REFERENCIA)
        {
            try
            {
                var dtReferencia = DateTime.ParseExact(DT_REFERENCIA, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                return Json(new SaldoProxy().BuscarBrutoPorPlanoReferenciaPessoa(dtReferencia, SQ_PLANO_PREVIDENCIAL, CdPessoa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
