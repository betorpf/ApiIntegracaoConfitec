using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Business.Sompo
{
    public interface ISolicitarInspecaoHandler
    {
        Task<SolicitarInspecaoResponse> Handle(SolicitarInspecaoRequest command);
    }
}
