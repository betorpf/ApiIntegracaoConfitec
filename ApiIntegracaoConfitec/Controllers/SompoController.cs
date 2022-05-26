using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
using ApiIntegracaoConfitec.Interfaces.Controller;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SompoController : ControllerBase
    {
        //POST: api/SolicitarInspecao
        [Route("SolicitarInspecao")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<IResult>> SolicitarInspecao(
                [FromServices] ISolicitarInspecaoHandler handler,
                [FromBody] SolicitarInspecaoRequest request)
        {
            try
            {
                SolicitarInspecaoResponse response = await handler.Handle(request);
                return this.Ok(response);
            }
            catch (System.Exception ex)
            {
                return this.BadRequest(new SolicitarInspecaoResponse()
                {
                    NumPI = request.PI,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Success = false
                }); ;
            }
        }

        //POST: api/CancelarInspecao
        [Route("CancelarInspecao")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<CancelarInspecaoResponse>> CancelarInspecao(
                [FromServices] ICancelarInspecaoHandler handler,
                [FromBody] CancelarInspecaoRequest request)
        {
            //TODO: Validar request
            try
            {
                CancelarInspecaoResponse response = await handler.Handle(request);
                return this.Ok(response);
            }
            catch (System.Exception ex)
            {
                return this.BadRequest(new CancelarInspecaoResponse()
                {
                    NumPI = request.PI,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Success = false
                });
            }

        }


    }
}
