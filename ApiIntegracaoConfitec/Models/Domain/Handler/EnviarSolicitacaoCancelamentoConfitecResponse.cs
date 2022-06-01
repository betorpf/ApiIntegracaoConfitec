using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoCancelamentoConfitecResponse
    {
        public ResponseCancelarInspecao response;

        public EnviarSolicitacaoCancelamentoConfitecResponse(ResponseCancelarInspecao response)
        {
            this.response = response;
        }
    }
}
