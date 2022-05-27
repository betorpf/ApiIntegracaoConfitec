using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class SolicitarAutenticacaoConfitecResponse
    {
        public ResponseToken responseToken;

        public SolicitarAutenticacaoConfitecResponse(ResponseToken responseToken)
        {
            this.responseToken = responseToken;
        }
    }
}
