using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaInspecaoRequest
    {
        public ConfitecSolicitarInspecao responseSolicitacaoInspecao { get; set; }

        public GravarRespostaInspecaoRequest(ConfitecSolicitarInspecao responseSolicitacaoInspecao)
        {
            this.responseSolicitacaoInspecao = responseSolicitacaoInspecao;
            
            if (responseSolicitacaoInspecao.erros != null && responseSolicitacaoInspecao.erros.Count > 0)
                throw new ConfitecErrorsException("Validação de dados retornados da Confitec", responseSolicitacaoInspecao.erros);
        }
    }
}
