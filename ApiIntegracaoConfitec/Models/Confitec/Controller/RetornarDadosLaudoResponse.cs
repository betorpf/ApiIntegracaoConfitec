using ApiIntegracaoConfitec.Interfaces.Controller;
using System.Collections.Generic;
using System.Net;

namespace ApiIntegracaoConfitec.Models.Confitec.Controller
{
    public class RetornarDadosLaudoResponse : IResultHttpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
