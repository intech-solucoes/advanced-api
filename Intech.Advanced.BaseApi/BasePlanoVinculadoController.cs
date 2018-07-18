#region Usings
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System; 
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BasePlanoVinculadoController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            try
            {
                return Json(new PlanoVinculadoProxy().ListarPorContratoTrabalho(SqContratoTrabalho));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porPlano/{plano}")]
        [Authorize("Bearer")]
        public IActionResult Get(int plano)
        {
            try
            {
                return Json(new PlanoVinculadoProxy().BuscarPorContratoTrabalhoPlano(SqContratoTrabalho, plano));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
