using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Interfaces.Controller;
using ApiIntegracaoConfitec.Models.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class CancelarInspecaoResponse : IResult
    {
        [Required(ErrorMessage = "Número do PI não informado")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um PI maior que zero")]
        public int NumPI { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public CancelarInspecaoResponse()
        {

        }
        public CancelarInspecaoResponse(int numPI)
        {
            this.NumPI = numPI;
            this.Message = ValidationUtility.ValidateObject(this);
            this.Success = true;

            if (!string.IsNullOrEmpty(this.Message))
                throw new System.Exception(message: this.Message);

        }
    }
}
