using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository
{
    public interface IDadosInspecaoSompoRepository
    {

        Task<DadosInspecao> RetornaDadosInspecao(string pi);
        Task<DadosCancelarInspecao> RetornaDadosCancelarInspecao(string pi);
        Task<DadosAutenticacao> RetornaDadosAutenticacao();
    }
}
