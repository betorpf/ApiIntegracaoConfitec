using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscarDadosAutenticacaoConfitecResponse
    {
        public DadosAutenticacao dadosAutenticacao;
        public bool Success { get; set; }
        public string Message { get; set; }

        public BuscarDadosAutenticacaoConfitecResponse(DadosAutenticacao dadosAutenticacao)
        {
            this.dadosAutenticacao = dadosAutenticacao;
            if (this.dadosAutenticacao != null)
                this.Success = true;
        }
    }
}
