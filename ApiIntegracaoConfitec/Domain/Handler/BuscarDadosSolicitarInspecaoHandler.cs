using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class BuscarDadosSolicitarInspecaoHandler : IBuscarDadosSolicitarInspecaoHandler
    {
        private readonly ISompoRepository _sompoRepository;

        public BuscarDadosSolicitarInspecaoHandler(ISompoRepository sompoRepository)
        {
            this._sompoRepository = sompoRepository;
        }

        public async Task<BuscarDadosSolicitarInspecaoResponse> Handle(BuscarDadosSolicitarInspecaoRequest request)
        {
            DadosInspecao dadosInspecao = await this._sompoRepository.RetornarDadosInspecao(request.Num_PI.ToString());

            BuscarDadosSolicitarInspecaoResponse response = new BuscarDadosSolicitarInspecaoResponse(dadosInspecao);

            return response;
        }
    }
}