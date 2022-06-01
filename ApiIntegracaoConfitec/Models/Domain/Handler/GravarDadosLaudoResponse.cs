using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarDadosLaudoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public GravarDadosLaudoResponse(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
            //var ListaValidacao = ValidationUtility.ListValidateObject(this.dadosLaudo);
            //if (ListaValidacao.Count > 0)
            //{
            //    throw new BRQValidationException("Validação nos dados do Laudo", ListaValidacao);
            //}
        }
    }
}
