using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Domain.Handler
{

    public interface IGravarRespostaInspecaoHandler
    {
        Task<GravarRespostaInspecaoResponse> Handle(GravarRespostaInspecaoRequest request);
    }

}
