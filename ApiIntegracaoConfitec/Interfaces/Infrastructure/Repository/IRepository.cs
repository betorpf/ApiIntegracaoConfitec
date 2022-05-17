using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository
{
    public interface IRepository<T>
    {

        Task<T> Get(string pi);

    }
}
