#region Usings
using Intech.Advanced.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System; 
#endregion

namespace Intech.Advanced.BaseApi
{
    public class BaseContrachequeController : BaseController
    {
        [HttpGet("datasPorPlano/{plano}")]
        [Authorize("Bearer")]
        public IActionResult GetDatasPorPlanoContratoTrabalho(int plano)
        {
            try
            {
                return Json(new ContrachequeProxy().BuscarDatasPorPlanoContratoTrabalho(SqContratoTrabalho, plano));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porPlano/{plano}")]
        [Authorize("Bearer")]
        public IActionResult GetPorPlanoContratoTrabalho(int plano)
        {
            try
            {
                return Json(new ContrachequeProxy().BuscarPorPlanoContratoTrabalho(SqContratoTrabalho, plano));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porPlanoCronograma/{plano}/{cronograma}")]
        [Authorize("Bearer")]
        public IActionResult GetPorPlanoContratoTrabalhoCronograma(int plano, int cronograma)
        {
            try
            {
                return Json(new ContrachequeProxy().BuscarRubricasPorPlanoContratoTrabalhoCronograma(SqContratoTrabalho, plano, cronograma, Pensionista, CdPessoa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}