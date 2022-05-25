using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarDadosLaudoResponse
    {
        public DadosLaudo dadosLaudo;
        public bool Success { get; set; }
        public string Message { get; set; }

        public GravarDadosLaudoResponse(DadosLaudo dadosLaudo)
        {
            this.dadosLaudo = dadosLaudo;
            this.Message = ValidationUtility.ValidateObject(this.dadosLaudo);
            this.Success = true;

            if (!string.IsNullOrEmpty(this.Message))
                throw new System.Exception(message: this.Message);
        }
    }
}
