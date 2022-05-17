using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Business.Sompo
{
    public interface ICancelarInspecaoHandler
    {
        Task<CancelarInspecaoResponse> Handle(CancelarInspecaoRequest command);
    }
    
}
