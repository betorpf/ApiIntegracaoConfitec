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

        //POST: api/SolicitarPedidoDeInspecao
        [Route("SolicitarPedidoDeInspecao")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<bool>> SolicitarPedidoDeInspecao(string inspecao)
        {
            return true;
        }

        //POST: api/VerificarLaudo
        [Route("VerificarLaudo")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<bool>> VerificarLaudo(string inspecao)
        {
            return true;
        }

        //POST: api/VerificarCancelamentos
        [Route("VerificarCancelamentos")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<bool>> VerificarCancelamentos(string inspecao)
        {
            return true;
        }

    }
}
