using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
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
        public async Task<ActionResult<SolicitarInspecaoResponse>> SolicitarInspecao(
                [FromServices] ISolicitarInspecaoHandler handler,
                [FromBody] SolicitarInspecaoRequest request)
        {

            //TODO: Validar request

            SolicitarInspecaoResponse response = await handler.Handle(request);

            return this.Ok(response);
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

            CancelarInspecaoResponse response = await handler.Handle(request);

            return this.Ok(response);
        }


    }
}
