using ApiIntegracaoConfitec.Interfaces.Controller;
using System.Collections.Generic;
using System.Net;

namespace ApiIntegracaoConfitec.Models.Confitec.Controller
{
    public class InformarDadosInspecaoResponse
    {
        public bool Success { get ; set ; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
