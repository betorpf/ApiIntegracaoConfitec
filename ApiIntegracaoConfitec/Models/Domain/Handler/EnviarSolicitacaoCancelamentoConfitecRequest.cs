using ApiIntegracaoConfitec.Models.Entity;
using System;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoCancelamentoConfitecRequest
    {
        public string access_token;
        public Int64 NumPI { get; set; }
        public bool Success { get; set; }

        public EnviarSolicitacaoCancelamentoConfitecRequest(Int64 numPI, string access_token)
        {
            this.NumPI = numPI;
            this.access_token = access_token;
            if (numPI != 0 && access_token != null)
                this.Success = true;

        }
    }
}
