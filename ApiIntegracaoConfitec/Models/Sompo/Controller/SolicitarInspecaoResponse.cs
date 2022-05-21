using ApiIntegracaoConfitec.Interfaces.Controller;
using ApiIntegracaoConfitec.Models.Entity;
using System.Net;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class SolicitarInspecaoResponse : IResult
    {
        public DadosInspecao dadosInspecao { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
