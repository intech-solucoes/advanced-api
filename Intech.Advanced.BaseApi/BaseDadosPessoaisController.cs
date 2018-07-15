#region Usings
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System; 
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BaseDadosPessoaisController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult GetPorCdpessoa()
        {
            try
            {
                return Json(new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
