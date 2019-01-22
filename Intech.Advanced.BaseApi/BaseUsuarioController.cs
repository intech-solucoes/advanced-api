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
    }
}