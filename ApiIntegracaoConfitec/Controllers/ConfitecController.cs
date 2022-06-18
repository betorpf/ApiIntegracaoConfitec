using ApiIntegracaoConfitec.Interfaces.Business.Confitec;
using ApiIntegracaoConfitec.Interfaces.Controller;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Confitec.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfitecController : Controller
    {
        //POST: Confitec/EnviarRetornoInspecao
        [Route("EnviarResultadoInspecao")]
        [HttpPost]
        public async Task<ActionResult<IResultHttpResponse>> EnviarResultadoInspecao(
                [FromServices] IEnviarRetornoLaudoHandler handler,
                [FromBody] ResultadoInspecaoRequest request)
        {
            RetornarDadosLaudoResponse response = await handler.Handle(request);
            return this.Ok(response);
        }
    }
}
