using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class GravarRespostaInspecaoHandler : IGravarRespostaInspecaoHandler
    {
        private readonly IDadosInspecaoSompoRepository _dadosInspecaoSompoRepository;

        public GravarRespostaInspecaoHandler(IDadosInspecaoSompoRepository dadosInspecaoSompoRepository)
        {
            this._dadosInspecaoSompoRepository = dadosInspecaoSompoRepository;
        }

        public async Task<GravarRespostaInspecaoResponse> Handle(GravarRespostaInspecaoRequest request)
        {
            bool resultado = await this._dadosInspecaoSompoRepository.GravarRetornoSolicitarInspecao(request.responseSolicitacaoInspecao);

            GravarRespostaInspecaoResponse response = new GravarRespostaInspecaoResponse(resultado);

            return response;
        }



        

    }
}
