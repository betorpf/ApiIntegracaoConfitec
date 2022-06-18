using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaInspecaoRequest
    {
        public ConfitecSolicitarInspecao responseSolicitacaoInspecao { get; set; }
        public Int64 Num_PI { get; set; }
        public int Num_Local { get; set; }
        public int Tip_Emissao { get; set; }

        public GravarRespostaInspecaoRequest(SolicitarInspecaoRequest solicitarInspecaoRequest, ConfitecSolicitarInspecao responseSolicitacaoInspecao)
        {
            this.responseSolicitacaoInspecao = responseSolicitacaoInspecao;
            this.Num_PI = solicitarInspecaoRequest.Num_PI;
            this.Num_Local = solicitarInspecaoRequest.Num_Local;
            this.Tip_Emissao = solicitarInspecaoRequest.Tip_Emissao;

            if (responseSolicitacaoInspecao.erros != null && responseSolicitacaoInspecao.erros.Count > 0)
                throw new ConfitecErrorsException("Validação de dados retornados da Confitec", responseSolicitacaoInspecao.erros);

        }
    }
}
