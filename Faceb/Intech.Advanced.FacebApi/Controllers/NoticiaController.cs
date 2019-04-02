#region Usings
using Intech.Advanced.BaseApi;
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq; 
#endregion

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : BaseController
    {
        [HttpGet("porPagina/{pagina}")]
        [AllowAnonymous]
        public IActionResult Get(int pagina)
        {
            try
            {
                var quantidadePorPagina = 6;

                var lista = new NoticiaProxy().Buscar()
                    .Skip((pagina - 1) * quantidadePorPagina)
                    .Take(quantidadePorPagina)
                    .ToList();

                return Json(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porOid/{oid}")]
        [AllowAnonymous]
        public IActionResult GetPorOid(decimal oid)
        {
            try
            {
                return Json(new NoticiaProxy().BuscarPorChave(oid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}