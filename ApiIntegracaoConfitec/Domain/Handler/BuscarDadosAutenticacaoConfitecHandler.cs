using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class BuscarDadosAutenticacaoConfitecHandler : IBuscarDadosAutenticacaoConfitecHandler
    {
        private readonly ISompoRepository _sompoRepository;

        public BuscarDadosAutenticacaoConfitecHandler(ISompoRepository sompoRepository)
        {
            this._sompoRepository = sompoRepository;
        }

        public async Task<BuscarDadosAutenticacaoConfitecResponse> Handle()
        {
            DadosAutenticacao dadosAutenticacao = await this._sompoRepository.RetornarDadosAutenticacao();

            BuscarDadosAutenticacaoConfitecResponse response = new BuscarDadosAutenticacaoConfitecResponse(dadosAutenticacao);

            return response;
        }
    }
}
