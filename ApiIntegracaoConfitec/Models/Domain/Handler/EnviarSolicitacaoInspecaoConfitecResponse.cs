using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoInspecaoConfitecResponse
    {
        public ConfitecSolicitarInspecao response;
        public EnviarSolicitacaoInspecaoConfitecResponse(ConfitecSolicitarInspecao response)
        {
            this.response = response;
        }
    }
}
