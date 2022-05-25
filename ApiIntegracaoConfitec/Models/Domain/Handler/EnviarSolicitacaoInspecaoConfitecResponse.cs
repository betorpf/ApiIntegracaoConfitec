using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoInspecaoConfitecResponse
    {
        public ResponseSolicitarInspecao response;
        public bool Success { get; set; }
        public string Message { get; set; }

        public EnviarSolicitacaoInspecaoConfitecResponse(ResponseSolicitarInspecao response)
        {
            this.response = response;
            this.Message = ValidationUtility.ValidateObject(this.response);
            this.Success = true;

            if (!string.IsNullOrEmpty(this.Message))
                throw new System.Exception(message: this.Message);
        }
    }
}
