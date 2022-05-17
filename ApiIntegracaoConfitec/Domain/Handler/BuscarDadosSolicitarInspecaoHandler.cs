using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class BuscarDadosSolicitarInspecaoHandler : IBuscarDadosSolicitarInspecaoHandler
    {
        private readonly IDadosInspecaoSompoRepository _dadosInspecaoSompoRepository;

        public BuscarDadosSolicitarInspecaoHandler(IDadosInspecaoSompoRepository dadosInspecaoSompoRepository)
        {
            this._dadosInspecaoSompoRepository = dadosInspecaoSompoRepository;
        }

        public async Task<BuscaDadosSolicitarInspecaoResponse> Handle(BuscaDadosSolicitarInspecaoRequest request)
        {
            var a = await this._dadosInspecaoSompoRepository.RetornaDadosInspecao(request.pi.ToString());

            BuscaDadosSolicitarInspecaoResponse response = new BuscaDadosSolicitarInspecaoResponse();
            response.Message = a.id_pi.ToString();

            return response;

        }
    }
}