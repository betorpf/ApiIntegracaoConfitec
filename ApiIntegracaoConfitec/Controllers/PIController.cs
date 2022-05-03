using ApiIntegracaoConfitec.Interfaces.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PIController : ControllerBase
    {
        private readonly IPIBusiness _piBusiness;

        public PIController(IPIBusiness piBusiness)
        {
            _piBusiness = piBusiness;
        }



        //GET: api/PI
        [HttpGet]
        //[Authorize] //TODO: VALIDAR
        public async Task<ActionResult<String>> GetPI(string inspecao)
        {
            var resultado = await _piBusiness.GetPI(inspecao);
            return resultado;
        }


    }
}
