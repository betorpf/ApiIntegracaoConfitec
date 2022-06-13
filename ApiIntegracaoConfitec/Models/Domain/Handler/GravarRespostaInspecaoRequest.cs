using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaInspecaoRequest
    {
        public ConfitecSolicitarInspecao responseSolicitacaoInspecao { get; set; }
        public int Num_PI { get; set; }
        public int Num_Local { get; set; }
        public int Tip_Emissao { get; set; }

        public GravarRespostaInspecaoRequest(int num_PI, int num_Local, int tip_Emissao, ConfitecSolicitarInspecao responseSolicitacaoInspecao)
        {
            this.responseSolicitacaoInspecao = responseSolicitacaoInspecao;
            this.Num_PI = num_PI;
            this.Num_Local = num_Local;
            this.Tip_Emissao = tip_Emissao;

            if (responseSolicitacaoInspecao.erros != null && responseSolicitacaoInspecao.erros.Count > 0)
                throw new ConfitecErrorsException("Validação de dados retornados da Confitec", responseSolicitacaoInspecao.erros);

        }
    }
}
