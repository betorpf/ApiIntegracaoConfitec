using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Interfaces.Controller;
using ApiIntegracaoConfitec.Models.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class CancelarInspecaoHttpResponse : IResultHttpResponse
    {
        public int NumPI { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        
        public CancelarInspecaoHttpResponse(int numPI)
        {
            this.NumPI = numPI;
        }
    }
}
