using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaInspecaoRequest
    {
        public ResponseSolicitarInspecao responseSolicitacaoInspecao { get; set; }

        public GravarRespostaInspecaoRequest(ResponseSolicitarInspecao responseSolicitacaoInspecao)
        {
            this.responseSolicitacaoInspecao = responseSolicitacaoInspecao;
        }
    }
}
