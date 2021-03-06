using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Domain.Handler
{
    public interface IBuscarDadosSolicitarInspecaoHandler
    {
        Task<BuscarDadosSolicitarInspecaoResponse> Handle(BuscarDadosSolicitarInspecaoRequest request);
    }
}
