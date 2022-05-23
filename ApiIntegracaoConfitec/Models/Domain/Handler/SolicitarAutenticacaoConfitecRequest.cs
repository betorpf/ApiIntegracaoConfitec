using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class SolicitarAutenticacaoConfitecRequest
    {
        public DadosAutenticacao dadosAutenticacao;
        public int PI { get; set; }
        public string Codigo { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public SolicitarAutenticacaoConfitecRequest(DadosAutenticacao dadosAutenticacao)
        {
            this.dadosAutenticacao = dadosAutenticacao;
            if (dadosAutenticacao != null)
                this.Success = true;

        }
    }
}
