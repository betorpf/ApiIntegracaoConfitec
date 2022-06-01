using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class GravarDadosLaudoHandler : IGravarDadosLaudoHandler
    {
        private readonly ISompoRepository _sompoRepository;

        public GravarDadosLaudoHandler(ISompoRepository sompoRepository)
        {
            this._sompoRepository = sompoRepository;
        }

        public async Task<GravarDadosLaudoResponse> Handle(GravarDadosLaudoRequest request)
        {
            QueryResult queryResult = await this._sompoRepository.GravarRetornarDadosLaudo(request.resultadoInspecao);

            GravarDadosLaudoResponse response = new GravarDadosLaudoResponse(queryResult.Success, queryResult.Message);

            return response;
        }

    }
}
