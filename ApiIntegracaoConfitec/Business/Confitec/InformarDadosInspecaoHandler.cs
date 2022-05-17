using ApiIntegracaoConfitec.Interfaces.Business.Confitec;
using ApiIntegracaoConfitec.Models.Confitec.Controller;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Business.Sompo
{
    public class InformarDadosInspecaoHandler : IInformaDadosInspecaoHandler
    {
        
        public InformarDadosInspecaoHandler()
        {
            
        }

        public async Task<InformarDadosInspecaoResponse> Handle(InformarDadosInspecaoRequest command)
        {

            return new InformarDadosInspecaoResponse
            {
                Success = true,
                Message = $"Sucesso"
            };
        }
    }
}
