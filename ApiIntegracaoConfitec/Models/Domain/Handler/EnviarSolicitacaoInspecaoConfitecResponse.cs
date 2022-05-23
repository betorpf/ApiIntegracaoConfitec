using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoInspecaoConfitecResponse
    {
        public ResponseSolicitarInspecao response;

        public EnviarSolicitacaoInspecaoConfitecResponse(ResponseSolicitarInspecao response)
        {
            this.response = response;
        }
    }
}
