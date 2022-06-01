using System.Diagnostics.CodeAnalysis;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    [ExcludeFromCodeCoverage]
    public class ErroConfitec
    {
        public string CodigoErro { get; set; }
        public string DescricaoErro { get; set; }
        public override string ToString()
        {
            return $"{CodigoErro}-{DescricaoErro}";
        }
    }
}
