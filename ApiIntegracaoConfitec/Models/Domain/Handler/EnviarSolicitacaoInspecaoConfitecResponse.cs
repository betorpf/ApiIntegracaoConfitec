using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoInspecaoConfitecResponse
    {
        private ResponseSolicitacaoInspecao response;

        public EnviarSolicitacaoInspecaoConfitecResponse(ResponseSolicitacaoInspecao response)
        {
            this.response = response;
        }
    }
}
