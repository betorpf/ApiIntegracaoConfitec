using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Business.Sompo
{
    public class CancelarInspecaoHandler : ICancelarInspecaoHandler
    {
        public Task<CancelarInspecaoResponse> Handle(CancelarInspecaoRequest command)
        {
            throw new System.NotImplementedException();
        }
    }
}
