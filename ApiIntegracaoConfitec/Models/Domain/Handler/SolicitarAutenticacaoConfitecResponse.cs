using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class SolicitarAutenticacaoConfitecResponse
    {
        public ResponseToken responseToken;
        public bool Success { get; set; }
        public string Message { get; set; }

        public SolicitarAutenticacaoConfitecResponse(ResponseToken responseToken)
        {
            this.responseToken = responseToken;
            this.Message = ValidationUtility.ValidateObject(this.responseToken);
            this.Success = true;

            if (!string.IsNullOrEmpty(this.Message))
                throw new System.Exception(message: this.Message);

        }
    }
}
