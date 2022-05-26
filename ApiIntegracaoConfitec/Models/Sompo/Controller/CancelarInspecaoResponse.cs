using ApiIntegracaoConfitec.Interfaces.Controller;
using System.Collections.Generic;
using System.Net;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class CancelarInspecaoResponse : IResult
    {
        public int NumPI { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
