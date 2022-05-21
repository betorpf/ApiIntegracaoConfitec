using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Service;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class EnviarSolicitacaoInspecaoConfitecHandler : IEnviarSolicitacaoInspecaoConfitecHandler
    {
        private readonly IConfitecService _confitecService;

        public EnviarSolicitacaoInspecaoConfitecHandler(IConfitecService confitecService)
        {
            this._confitecService = confitecService;
        }

        public async Task<EnviarSolicitacaoInspecaoConfitecResponse> Handle(EnviarSolicitacaoInspecaoConfitecRequest request)
        {
            RequestSolicitacaoInspecao requestSolicitacaoInspecao = new RequestSolicitacaoInspecao(request.dadosInspecao);

            ResponseSolicitacaoInspecao response = await this._confitecService.SolicitarInspecao(requestSolicitacaoInspecao);
            
            return new EnviarSolicitacaoInspecaoConfitecResponse(response);
        }
    }
}
