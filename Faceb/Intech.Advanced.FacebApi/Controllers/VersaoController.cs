#region Usings
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection; 
#endregion

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VersaoController : Controller
    {
        public IActionResult Get()
        {
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName();
                return Json(version.Version.ToString(3));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}