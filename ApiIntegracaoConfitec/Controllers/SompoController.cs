using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SompoController : ControllerBase
    {
        public SompoController()
        {

        }

        //POST: api/SolicitarInspecao
        [Route("SolicitarInspecao")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<bool>> SolicitarInspecao(string inspecao)
        {
            return true;
        }

        //POST: api/CancelarInspecao
        [Route("CancelarInspecao")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<bool>> CancelarInspecao(string inspecao)
        {
            return true;
        }

    }
}
