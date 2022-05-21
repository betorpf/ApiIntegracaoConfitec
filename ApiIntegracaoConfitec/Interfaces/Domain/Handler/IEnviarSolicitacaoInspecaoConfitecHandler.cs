using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Domain.Handler
{
    public interface IEnviarSolicitacaoInspecaoConfitecHandler
    {
        Task<EnviarSolicitacaoInspecaoConfitecResponse> Handle(EnviarSolicitacaoInspecaoConfitecRequest request);
    }
}
