using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class GravarRespostaInspecaoHandler : IGravarRespostaInspecaoHandler
    {
        private readonly ISompoRepository _sompoRepository;

        public GravarRespostaInspecaoHandler(ISompoRepository sompoRepository)
        {
            this._sompoRepository = sompoRepository;
        }

        public async Task<GravarRespostaInspecaoResponse> Handle(GravarRespostaInspecaoRequest request)
        {
            try
            {
                bool resultado = await this._sompoRepository.GravarRetornoSolicitarInspecao(request.responseSolicitacaoInspecao);

                GravarRespostaInspecaoResponse response = new GravarRespostaInspecaoResponse(resultado, "");

                return response;
            }
            catch (System.Exception ex)
            {
                return new GravarRespostaInspecaoResponse(false, ex.Message);
            }
        }
    }
}
