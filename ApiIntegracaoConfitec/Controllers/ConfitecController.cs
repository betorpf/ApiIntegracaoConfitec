using ApiIntegracaoConfitec.Interfaces.Business.Confitec;
using ApiIntegracaoConfitec.Interfaces.Controller;
using ApiIntegracaoConfitec.Models.Confitec.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfitecController : Controller
    {
        //POST: api/EnviarRetornoLaudo
        [Route("EnviarRetornoLaudo")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<IResultHttpResponse>> EnviarRetornoLaudo(
                [FromServices] IEnviarRetornoLaudoHandler handler,
                [FromBody] RetornarDadosLaudoRequest request)
        {
            RetornarDadosLaudoResponse response = new();

            try
            {
                response = await handler.Handle(request);
            }
            catch (System.Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = ex.Message;
                return this.BadRequest(response);
            }

            return this.Ok(response);

        }
    }
}
