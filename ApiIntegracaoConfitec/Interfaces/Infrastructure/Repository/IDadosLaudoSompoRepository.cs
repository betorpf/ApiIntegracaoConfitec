using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository
{
    public interface IDadosLaudoSompoRepository
    {
        Task<DadosLaudo> RetornarDadosLaudo(string pi);
    }
}
