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
        //POST: Sompo/SolicitarInspecao
        [Route("SolicitarInspecao")]
        [HttpPost]
        public async Task<ActionResult<IResultHttpResponse>> SolicitarInspecao(
                [FromServices] ISolicitarInspecaoHandler handler,
                [FromBody] SolicitarInspecaoRequest request)
        {
            SolicitarInspecaoHttpResponse response = await handler.Handle(request);
            return this.Ok(response);
        }

        //POST: Sompo/CancelarInspecao
        [Route("CancelarInspecao")]
        [HttpPost]
        public async Task<ActionResult<IResultHttpResponse>> CancelarInspecao(
                [FromServices] ICancelarInspecaoHandler handler,
                [FromBody] CancelarInspecaoRequest request)
        {
                CancelarInspecaoHttpResponse response = await handler.Handle(request);
                return this.Ok(response);
        }
    }
}
