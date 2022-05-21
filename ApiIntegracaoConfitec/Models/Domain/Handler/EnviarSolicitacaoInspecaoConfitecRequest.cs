using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoInspecaoConfitecRequest
    {
        public DadosInspecao dadosInspecao;
        public DadosAutenticacao dadosAutenticacao;
        public int PI { get; set; }
        public string Codigo { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public EnviarSolicitacaoInspecaoConfitecRequest(DadosInspecao dadosInspecao, DadosAutenticacao dadosAutenticacao)
        {
            this.dadosInspecao = dadosInspecao;
            this.dadosAutenticacao = dadosAutenticacao;
            if (dadosInspecao != null && dadosAutenticacao != null)
                this.Success = true;
            
        }
    }
}
