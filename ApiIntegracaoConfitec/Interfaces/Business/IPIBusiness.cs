using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Business
{
    public interface IPIBusiness
    {
        Task<string> GetPI(string inspecao);
    }
}
