#region Usings
using Intech.Advanced.BaseApi;
using Intech.Advanced.Negocio.Proxy;
using Intech.Lib.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Intech.Advanced.FacebApi.Controllers
{
    [Route(RotasApi.Usuario)]
    [ApiController]
    public class UsuarioController : BaseUsuarioController
    {
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
                    var dadosPessoais = new DadosPessoaisProxy().BuscarPorCdPessoa(usuario.CD_PESSOA.Value).First();

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

        [HttpPost("v2/login")]
        [AllowAnonymous]
        public IActionResult LoginV2(
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations,
            [FromBody] dynamic user)
        {
            try
            {
                throw new Exception("Por favor, atualize seu aplicativo.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("v3/login")]
        [AllowAnonymous]
        public IActionResult LoginV3(
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
                    var grupo = new UsuarioGrupoProxy().BuscarPorUsuario(usuario.USR_CODIGO);
                    bool pensionista = grupo != null && grupo.GRP_CODIGO == 32 ? true : false;

                    string sqContratoTrabalho;

                    if (pensionista)
                    {
                        var processo = new ProcessoBeneficioProxy().BuscarPorCdPessoa(usuario.CD_PESSOA.Value);
                        sqContratoTrabalho = processo.SQ_CONTRATO_TRABALHO.ToString();
                    }
                    else
                    {
                        var dadosPessoais = new DadosPessoaisProxy().BuscarPorCdPessoa(usuario.CD_PESSOA.Value).First();
                        sqContratoTrabalho = dadosPessoais.SQ_CONTRATO_TRABALHO.ToString();
                    }

                    var claims = new List<KeyValuePair<string, string>> {
                        new KeyValuePair<string, string>("Cpf", usuario.USR_LOGIN),
                        new KeyValuePair<string, string>("CdPessoa", usuario.CD_PESSOA.ToString()),
                        new KeyValuePair<string, string>("Admin", usuario.USR_ADMINISTRADOR),
                        new KeyValuePair<string, string>("SqContratoTrabalho", sqContratoTrabalho),
                        new KeyValuePair<string, string>("Pensionista", pensionista.ToString())
                    };

                    var token = AuthenticationToken.Generate(signingConfigurations, tokenConfigurations, usuario.USR_LOGIN, claims);
                    return Json(new
                    {
                        token.AccessToken,
                        token.Authenticated,
                        token.Created,
                        token.Expiration,
                        token.Message,
                        pensionista
                    });

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

        [HttpGet("[action]")]
        [Authorize("Bearer")]
        public IActionResult BuscarMatriculas()
        {
            try
            {
                var contratosTrabalho = new ContratoTrabalhoProxy().BuscarPorCpfComPlanoAtivo(Cpf);
                var listaMatriculas = contratosTrabalho
                    .GroupBy(x => new { x.NR_REGISTRO, x.NO_EMPRESA })
                    .Select(x => new
                    {
                        Matricula = x.Key.NR_REGISTRO,
                        Empresa = x.Key.NO_EMPRESA
                    })
                    .ToList();

                return Ok(listaMatriculas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{matricula}")]
        [Authorize("Bearer")]
        public IActionResult SelecionarMatricula(
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations,
            string matricula)
        {
            try
            {
                bool pensionista = false;

                var usuario = new UsuarioProxy().BuscarPorCPF(Cpf);

                var contratoTrabalho = new ContratoTrabalhoProxy().BuscarPorCpfComPlanoAtivo(Cpf).First(x => x.NR_REGISTRO == matricula);
                var cdPessoa = contratoTrabalho.CD_PESSOA;
                var sqContratoTrabalho = contratoTrabalho.SQ_CONTRATO_TRABALHO;

                var processo = new ProcessoBeneficioProxy().BuscarPorCdPessoa(cdPessoa);
                if (processo != null)
                {
                    pensionista = true;
                    sqContratoTrabalho = processo.SQ_CONTRATO_TRABALHO.Value;
                }

                var claims = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("Cpf", Cpf),
                    new KeyValuePair<string, string>("CdPessoa", cdPessoa.ToString()),
                    new KeyValuePair<string, string>("Admin", Admin ? "S" : "N"),
                    new KeyValuePair<string, string>("SqContratoTrabalho", sqContratoTrabalho.ToString()),
                    new KeyValuePair<string, string>("Pensionista", pensionista.ToString())
                };

                var token = AuthenticationToken.Generate(signingConfigurations, tokenConfigurations, Cpf, claims);

                return Ok(new
                {
                    token.AccessToken,
                    Pensionista = pensionista
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
