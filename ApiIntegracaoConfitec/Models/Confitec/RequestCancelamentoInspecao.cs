using ApiIntegracaoConfitec.Interfaces.Services;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class RequestCancelamentoInspecao : IRequest
    {
        public int NumPI { get; set; }

        public RequestCancelamentoInspecao (int NumPI)
        {
            this.NumPI = NumPI;
        }
    }
}
