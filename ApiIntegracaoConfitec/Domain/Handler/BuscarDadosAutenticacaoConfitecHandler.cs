using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Handler
{
    public class BuscarDadosAutenticacaoConfitecHandler : IBuscarDadosAutenticacaoConfitecHandler
    {
        private readonly IDadosInspecaoSompoRepository _dadosInspecaoSompoRepository;

        public BuscarDadosAutenticacaoConfitecHandler(IDadosInspecaoSompoRepository dadosInspecaoSompoRepository)
        {
            this._dadosInspecaoSompoRepository = dadosInspecaoSompoRepository;
        }

        public async Task<BuscarDadosAutenticacaoConfitecResponse> Handle()
        {
            DadosAutenticacao dadosAutenticacao = await this._dadosInspecaoSompoRepository.RetornaDadosAutenticacao();

            BuscarDadosAutenticacaoConfitecResponse response = new BuscarDadosAutenticacaoConfitecResponse(dadosAutenticacao);

            return response;
        }
    }
}
