using ApiIntegracaoConfitec.Interfaces.Controller;
using System.Net;

namespace ApiIntegracaoConfitec.Models.Confitec.Controller
{
    public class InformarDadosInspecaoResponse : IResult
    {
        public bool Success { get ; set ; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
