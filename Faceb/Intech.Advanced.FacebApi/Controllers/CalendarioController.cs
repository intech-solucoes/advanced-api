using Intech.Advanced.BaseApi;
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarioController : BaseController
    {
        [HttpGet("[action]/{plano}")]
        [AllowAnonymous]
        public IActionResult BuscarPorPlano(int plano)
        {
            return Json(new CalendarioPgtProxy().BuscarPorPlano(plano.ToString()));
        }
    }
}