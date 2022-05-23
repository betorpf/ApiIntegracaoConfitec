using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository
{
    public interface IDadosInspecaoSompoRepository
    {

        Task<DadosInspecao> RetornarDadosInspecao(string pi);
        Task<DadosCancelarInspecao> RetornarDadosCancelarInspecao(string pi);
        Task<DadosAutenticacao> RetornarDadosAutenticacao();
        Task<bool> GravarRetornoSolicitarInspecao(ResponseSolicitarInspecao responseSolicitarInspecao);
    }
}
