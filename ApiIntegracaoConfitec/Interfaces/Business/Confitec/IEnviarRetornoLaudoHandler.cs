using ApiIntegracaoConfitec.Models.Confitec.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Business.Confitec
{
    public interface IEnviarRetornoLaudoHandler
    {
        Task<RetornarDadosLaudoResponse> Handle(RetornarDadosLaudoRequest command);
    }
}
