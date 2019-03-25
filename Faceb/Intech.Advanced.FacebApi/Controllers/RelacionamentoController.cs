#region Usings
using Intech.Advanced.BaseApi;
using Intech.Advanced.Entidades;
using Intech.Advanced.Negocio.Proxy;
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
    public class RelacionamentoController : BaseController
    {
        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody]EmailEntidade relacionamentoEntidade)
        {
            try
            {
                DadosPessoaisEntidade dadosPessoais;

                if (Pensionista)
                    dadosPessoais = new DadosPessoaisProxy().BuscarPensionistaPorCdPessoa(CdPessoa);
                else
                    dadosPessoais = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa);

                var emailConfig = AppSettings.Get().Email;
                var corpoEmail = 
                    $"Nome: {dadosPessoais.NO_PESSOA}<br/>" +
                    $"E-mail: {relacionamentoEntidade.Email}<br/>" +
                    $"Mensagem: {relacionamentoEntidade.Mensagem}";
                EnvioEmail.Enviar(emailConfig, emailConfig.EmailRelacionamento, $"Faceb - {relacionamentoEntidade.Assunto}", corpoEmail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro ao enviar e-mail");
            }
        }
    }
}