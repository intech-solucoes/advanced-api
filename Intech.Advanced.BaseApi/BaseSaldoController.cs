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
        [HttpGet("saldoBD")]
        [Authorize("Bearer")]
        public IActionResult GetSaldoBD()
        {
            try
            {
                return Json((new SaldoProxy().BuscarSaldoBD(DateTime.Now, SqContratoTrabalho, 1, CdPessoa)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("saldoCD")]
        [Authorize("Bearer")]
        public IActionResult GetSaldoCD()
        {
            try
            {
                return Json(new SaldoProxy().BuscarSaldoCD(DateTime.Now, SqContratoTrabalho, 3, CdPessoa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("brutoPorPlanoDataReferencia/{SQ_PLANO_PREVIDENCIAL}/{DT_REFERENCIA}")]
        [Authorize("Bearer")]
        public IActionResult GetBrutoPorPlanoDataReferenciaPessoa(int SQ_PLANO_PREVIDENCIAL, string DT_REFERENCIA)
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
