using ApiIntegracaoConfitec.Models.Confitec.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Business.Confitec
{
    public interface IInformaDadosInspecaoHandler
    {
        Task<InformarDadosInspecaoResponse> Handle(InformarDadosInspecaoRequest command);
    }
}
