#region Usings
using Intech.Advanced.Entidades;
using Intech.Advanced.Negocio.Proxy;
using Intech.Lib.Web.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BaseUsuarioController : Controller
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            try
            {
                var cpf = User.Identity.Name;
                return Json(new UsuarioProxy().BuscarPorCPF(cpf));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations,
            [FromBody] dynamic user)
        {
            try
            {
                string cpf = user.Cpf.Value;
                string senha = user.Senha.Value;

                var usuario = new UsuarioProxy().BuscarPorLoginSenha(cpf, senha);

                if (usuario != null)
                {
                    var dadosPessoais = new DadosPessoaisProxy().BuscarPorCdPessoa(usuario.CD_PESSOA.Value);

                    var claims = new List<KeyValuePair<string, string>> {
                        new KeyValuePair<string, string>("Cpf", usuario.USR_LOGIN),
                        new KeyValuePair<string, string>("CdPessoa", usuario.CD_PESSOA.ToString()),
                        new KeyValuePair<string, string>("Admin", usuario.USR_ADMINISTRADOR),
                        new KeyValuePair<string, string>("SqContratoTrabalho", dadosPessoais.SQ_CONTRATO_TRABALHO.ToString())
                    };

                    return Json(AuthenticationToken.Generate(signingConfigurations, tokenConfigurations, usuario.USR_LOGIN, claims));
                }
                else
                {
                    throw new Exception("Matrícula ou senha incorretos!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
