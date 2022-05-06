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

        //POST: api/ReceberNumeroInspecao
        [Route("ReceberNumeroInspecao")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<bool>> ReceberNumeroInspecao(string inspecao)
        {
            return true;
        }

        //POST: api/ReceberLaudo
        [Route("ReceberLaudo")]
        [HttpPost]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<bool>> ReceberLaudo(string inspecao)
        {
            return true;
        }



    }
}
