using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Domain.Handler
{
    public interface IGravarDadosLaudoHandler
    {
        Task<GravarDadosLaudoResponse> Handle(ResultadoInspecaoRequest request);
    }
}
