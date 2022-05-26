using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoCancelamentoConfitecResponse
    {
        public ResponseCancelarInspecao response;
        public bool Success { get; set; }
        public string Message { get; set; }

        public EnviarSolicitacaoCancelamentoConfitecResponse(ResponseCancelarInspecao response)
        {
            this.response = response;
            this.Message = ValidationUtility.ValidateObject(this.response);
            this.Success = true;

            if (!string.IsNullOrEmpty(this.Message))
                throw new System.Exception(message: this.Message);
        }
    }
}
