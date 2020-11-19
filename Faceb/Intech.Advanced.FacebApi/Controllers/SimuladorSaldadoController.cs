using Intech.Advanced.BaseApi;
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimuladorSaldadoController : BaseController
    {

        [HttpGet("[action]")]
        [Authorize("Bearer")]
        public IActionResult Simular()
        {
            try
            {
                var result = new HistValoresProcessoProxy().BuscarPorCpfPlano(Cpf, 4);

                return Json(result);
            }
             catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
