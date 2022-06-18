using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Interfaces.Controller;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class SolicitarInspecaoHttpResponse : IResultHttpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public SolicitarInspecaoHttpResponse()
        {
            this.Errors = new List<string>();
        }
    }
}
