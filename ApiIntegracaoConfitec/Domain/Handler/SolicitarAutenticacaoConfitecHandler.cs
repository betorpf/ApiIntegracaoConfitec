using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Service;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class SolicitarAutenticacaoConfitecHandler : ISolicitarAutenticacaoConfitecHandler
    {
        private readonly IConfitecService _confitecService;

        public SolicitarAutenticacaoConfitecHandler(IConfitecService confitecService)
        {
            this._confitecService = confitecService;
        }

        public async Task<SolicitarAutenticacaoConfitecResponse> Handle(SolicitarAutenticacaoConfitecRequest request)
        {
            RequestToken requestToken = new RequestToken(request.dadosAutenticacao.SompoUsername, request.dadosAutenticacao.SompoPassword);

            ResponseToken response = await this._confitecService.Autenticacao(requestToken);

            return new SolicitarAutenticacaoConfitecResponse(response);
        }
    }
}
