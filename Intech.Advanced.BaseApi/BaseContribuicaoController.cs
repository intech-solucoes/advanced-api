#region Usings
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System; 
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BaseContribuicaoController : BaseController
    {
        [HttpGet("porPlano/{plano}")]
        [Authorize("Bearer")]
        public IActionResult GetPorPlano(int plano)
        {
            try
            {
                return Json(new ContribuicaoProxy().BuscarPorPlanoContratoTrabalho(SqContratoTrabalho, plano));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
