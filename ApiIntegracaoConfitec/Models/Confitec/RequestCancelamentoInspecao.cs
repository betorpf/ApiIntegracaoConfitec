using ApiIntegracaoConfitec.Interfaces.Services;
using System;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class RequestCancelamentoInspecao : IRequest
    {
        public Int64 NumPI { get; set; }

        public RequestCancelamentoInspecao (Int64 NumPI)
        {
            this.NumPI = NumPI;
        }
    }
}
