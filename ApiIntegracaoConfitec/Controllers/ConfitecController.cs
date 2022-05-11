using ApiIntegracaoConfitec.Models.Confitec;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfitecController : ControllerBase
    {

        public ConfitecController()
        {

        }

        //POST: api/EnviarRetornoInspecao
        [Route("EnviarRetornoInspecao")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<bool>> EnviarRetornoInspecao(ResponseSolicitacaoInspecao retornoLaudo)
        {
            return true;
        }

    }
}
