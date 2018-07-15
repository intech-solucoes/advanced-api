#region Usings
using Intech.Advanced.BaseApi;
using Intech.Lib.Util.Email;
using Intech.Lib.Web;
using Intech.Lib.Web.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System; 
#endregion

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route(RotasApi.Relacionamento)]
    [ApiController]
    public class RelacionamentoController : ControllerBase
    {
        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody]EmailEntidade relacionamentoEntidade)
        {
            try
            {
                var emailConfig = AppSettings.Get().Email;
                EnvioEmail.Enviar(emailConfig, relacionamentoEntidade.Email, $"Faceb - {relacionamentoEntidade.Assunto}", relacionamentoEntidade.Mensagem);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro ao enviar e-mail");
            }
        }
    }
}