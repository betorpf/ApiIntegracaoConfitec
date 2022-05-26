using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Domain.Handler;
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
            try
            {
                bool resultado = await this._sompoRepository.GravarRetornoCancelarInspecao(request.responseCancelarInspecao);

                GravarRespostaCancelamentoResponse response = new GravarRespostaCancelamentoResponse(resultado, "");

                return response;
            }
            catch (System.Exception ex)
            {
                return new GravarRespostaCancelamentoResponse(false, ex.Message);
            }
        }
    }
}
