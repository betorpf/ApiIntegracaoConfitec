using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Service;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class EnviarSolicitacaoCancelamentoConfitecHandler : IEnviarSolicitacaoCancelamentoConfitecHandler
    {
        private readonly IConfitecService _confitecService;

        public EnviarSolicitacaoCancelamentoConfitecHandler(IConfitecService confitecService)
        {
            this._confitecService = confitecService;
        }

        public async Task<EnviarSolicitacaoCancelamentoConfitecResponse> Handle(EnviarSolicitacaoCancelamentoConfitecRequest request)
        {
            RequestCancelamentoInspecao requestSolicitacaoCancelamento = new RequestCancelamentoInspecao(request.NumPI);

            ResponseCancelarInspecao response = await this._confitecService.CancelarInspecao(requestSolicitacaoCancelamento, request.access_token);

            return new EnviarSolicitacaoCancelamentoConfitecResponse(response);
        }
    }
}
