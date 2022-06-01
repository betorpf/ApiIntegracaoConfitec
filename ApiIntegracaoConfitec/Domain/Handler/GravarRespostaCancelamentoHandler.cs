using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class GravarRespostaCancelamentoHandler : IGravarRespostaCancelamentoHandler
    {
        private readonly ISompoRepository _sompoRepository;

        public GravarRespostaCancelamentoHandler(ISompoRepository sompoRepository)
        {
            this._sompoRepository = sompoRepository;
        }

        public async Task<GravarRespostaCancelamentoResponse> Handle(GravarRespostaCancelamentoRequest request)
        {
            QueryResult queryResult = await this._sompoRepository.GravarRetornoCancelarInspecao(request.responseCancelarInspecao);

            GravarRespostaCancelamentoResponse response = new GravarRespostaCancelamentoResponse(queryResult.Success, queryResult.Message);

            return response;
        }
    }
}
