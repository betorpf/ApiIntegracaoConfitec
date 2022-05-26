using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Service
{
    public interface IConfitecService
    {

        Task<ResponseToken> Autenticacao(RequestToken requestToken);

        Task<ResponseSolicitarInspecao> SolicitarInspecao(RequestSolicitacaoInspecao pedidoInspecao, string access_token = null);

        Task<ResponseCancelarInspecao> CancelarInspecao(RequestCancelamentoInspecao requestCancelamentoInspecao, string access_token = null);

        Task<string> GenericPost(string method, string jsonContent, string access_token = null);

    }
}