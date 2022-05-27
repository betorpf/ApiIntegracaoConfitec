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
            var ListaValidacao = ValidationUtility.ListValidateObject(this.responseSolicitacaoInspecao);
            if (ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Validação dos dados recebidos da Confitec", ListaValidacao);
            }
        }
    }
}
