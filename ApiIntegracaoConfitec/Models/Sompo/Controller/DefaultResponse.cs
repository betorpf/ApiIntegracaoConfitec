using ApiIntegracaoConfitec.Interfaces.Controller;
using System.Collections.Generic;
using System.Net;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class DefaultResponse : IResult
    {
        public bool Success { get ; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
