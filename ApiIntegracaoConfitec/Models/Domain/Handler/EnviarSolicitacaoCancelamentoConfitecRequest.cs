using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoCancelamentoConfitecRequest
    {
        public string access_token;
        public int NumPI { get; set; }
        public string Codigo { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public EnviarSolicitacaoCancelamentoConfitecRequest(int numPI, string access_token)
        {
            this.NumPI = numPI;
            this.access_token = access_token;
            if (numPI != 0 && access_token != null)
                this.Success = true;

        }
    }
}
