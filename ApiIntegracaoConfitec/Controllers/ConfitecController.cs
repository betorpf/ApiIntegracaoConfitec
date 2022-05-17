using ApiIntegracaoConfitec.Interfaces.Business.Confitec;
using ApiIntegracaoConfitec.Models.Confitec.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfitecController : Controller
    {
        //POST: api/EnviarRetornoInspecao
        [Route("EnviarRetornoInspecao")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<InformarDadosInspecaoResponse>> EnviarRetornoInspecao(
                [FromServices] IInformaDadosInspecaoHandler handler,
                [FromBody] InformarDadosInspecaoRequest request)
        {

            //TODO: Validar request

            InformarDadosInspecaoResponse response = await handler.Handle(request);

            return this.Ok(response);
        }
    }
}
