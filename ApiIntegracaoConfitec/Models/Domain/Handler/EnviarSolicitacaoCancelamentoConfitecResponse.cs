using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
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
            var ListaValidacao = ValidationUtility.ListValidateObject(this.response);
            if (ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Validação de dados retornados da Confitec", ListaValidacao);
            }
        }
    }
}
