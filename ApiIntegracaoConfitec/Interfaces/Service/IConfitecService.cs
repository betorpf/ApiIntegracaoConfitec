using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Service
{
    public interface IConfitecService
    {

        Task<ResponseToken> Autenticacao(RequestToken requestToken);

        Task<ResponseSolicitacaoInspecao> SolicitarInspecao(RequestSolicitacaoInspecao pedidoInspecao);

        Task<ResponseCancelamentoInspecao> CancelarInspecao(RequestCancelamentoInspecao requestCancelamentoInspecao);

        Task<string> GenericPost(string method, string jsonContent, string access_token = null);

    }
}