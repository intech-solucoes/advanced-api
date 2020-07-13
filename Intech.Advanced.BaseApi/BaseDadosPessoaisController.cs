#region Usings
using Intech.Advanced.Entidades;
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BaseDadosPessoaisController : BaseController
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            try
            {
                DadosPessoaisEntidade dadosPessoais;

                if (Pensionista)
                    dadosPessoais = new DadosPessoaisProxy().BuscarPensionistaPorCdPessoa(CdPessoa);
                else
                    dadosPessoais = new DadosPessoaisProxy().BuscarPorCdPessoa(CdPessoa).First();

                return Json(dadosPessoais);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}