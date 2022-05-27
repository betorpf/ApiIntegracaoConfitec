using ApiIntegracaoConfitec.Interfaces.Controller;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Business.Sompo
{
    public interface ISolicitarInspecaoHandler
    {
        Task<SolicitarInspecaoHttpResponse> Handle(SolicitarInspecaoRequest command);
    }
}
