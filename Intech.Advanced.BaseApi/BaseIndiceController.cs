#region Usings
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization; 
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BaseIndiceController : BaseController
    {
        [HttpGet("porCdIndice/{cdIndice}")]
        [Authorize("Bearer")]
        public IActionResult GetPorCdIndice(string cdIndice)
        {
            try
            {
                return Json(new IndiceProxy().BuscarPorCdIndice(cdIndice));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porCdIndicePeriodo/{cdIndice}/{dtIni}/{dtFim}")]
        [Authorize("Bearer")]
        public IActionResult GetPorCdIndice(string cdIndice, string dtIni, string dtFim)
        {
            try
            {
                var dataInicio = DateTime.ParseExact(dtIni, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                var dataFim = DateTime.ParseExact(dtFim, "dd.MM.yyyy", new CultureInfo("pt-BR"));

                return Json(new IndiceProxy().BuscarPorCdIndicePeriodo(cdIndice, dataInicio, dataFim));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
