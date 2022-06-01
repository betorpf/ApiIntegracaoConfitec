using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarDadosLaudoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public GravarDadosLaudoResponse(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;

            if (!this.Success)
            {
                throw new BRQValidationException(this.Message);
            }
        }
    }
}
