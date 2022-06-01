using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository
{
    public interface ISompoRepository
    {
        // Solicitar inspeção
        Task<DadosInspecao> RetornarDadosInspecao(string pi);
        Task<DadosAutenticacao> RetornarDadosAutenticacao();
        Task<QueryResult> GravarRetornoSolicitarInspecao(ConfitecSolicitarInspecao responseSolicitarInspecao);
        // Gravar laudo
        Task<QueryResult> GravarRetornarDadosLaudo(ResultadoInspecao resultadoInspecao);
        // Cancelar inspeção
        Task<QueryResult> GravarRetornoCancelarInspecao(ResponseCancelarInspecao responseCancelarInspecao);
    }
}
