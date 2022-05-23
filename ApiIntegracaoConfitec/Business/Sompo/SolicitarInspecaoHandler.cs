using ApiIntegracaoConfitec.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Business.Sompo
{
    public class SolicitarInspecaoHandler : ISolicitarInspecaoHandler
    {
        private readonly IBuscarDadosSolicitarInspecaoHandler _buscarDadosSolicitarInspecaoHandler;
        private readonly IEnviarSolicitacaoInspecaoConfitecHandler _enviarSolicitacaoInspecaoConfitecHandler;
        private readonly IBuscarDadosAutenticacaoConfitecHandler _buscarDadosAutenticacaoConfitecHandler;
        private readonly ISolicitarAutenticacaoConfitecHandler _solicitarAutenticacaoConfitecHandler;
        private readonly IGravarRespostaInspecaoHandler _gravarRespostaInspecaoHandler;

        public SolicitarInspecaoHandler(
                IBuscarDadosSolicitarInspecaoHandler buscarDadosSolicitarInspecaoHandler = null,
                IEnviarSolicitacaoInspecaoConfitecHandler enviarSolicitacaoInspecaoConfitecHandler = null, 
                IBuscarDadosAutenticacaoConfitecHandler buscarDadosAutenticacaoConfitecHandler = null,
                ISolicitarAutenticacaoConfitecHandler solicitarAutenticacaoConfitecHandler = null,
                IGravarRespostaInspecaoHandler gravarRespostaInspecaoHandler = null)
        {
            this._buscarDadosSolicitarInspecaoHandler = buscarDadosSolicitarInspecaoHandler;
            this._enviarSolicitacaoInspecaoConfitecHandler = enviarSolicitacaoInspecaoConfitecHandler;
            this._buscarDadosAutenticacaoConfitecHandler = buscarDadosAutenticacaoConfitecHandler;
            this._solicitarAutenticacaoConfitecHandler = solicitarAutenticacaoConfitecHandler;
            this._gravarRespostaInspecaoHandler = gravarRespostaInspecaoHandler;
        }

        public async Task<SolicitarInspecaoResponse> Handle(SolicitarInspecaoRequest command)
        {

            BuscarDadosSolicitarInspecaoResponse buscaDadosSolicitarInspecaoResponse = await this._buscarDadosSolicitarInspecaoHandler.Handle(new BuscarDadosSolicitarInspecaoRequest() { pi = command.PI });

            //TODO: 1.1: Validar se as informações estão certas

            //TODO: 2.1: Dados da autenticação
            BuscarDadosAutenticacaoConfitecResponse buscarDadosAutenticacaoConfitecResponse = await this._buscarDadosAutenticacaoConfitecHandler.Handle();

            //TODO: 2.2: Chamar serviço Confitec de Autenticação
            SolicitarAutenticacaoConfitecRequest solicitarAutenticacaoConfitecRequest = new SolicitarAutenticacaoConfitecRequest(buscarDadosAutenticacaoConfitecResponse.dadosAutenticacao);
            SolicitarAutenticacaoConfitecResponse solicitarAutenticacaoConfitecResponse = await this._solicitarAutenticacaoConfitecHandler.Handle(solicitarAutenticacaoConfitecRequest);

            //TODO: 2.3: Chamar serviço Confitec
            EnviarSolicitacaoInspecaoConfitecRequest enviarSolicitacaoInspecaoConfitecRequest = new EnviarSolicitacaoInspecaoConfitecRequest(buscaDadosSolicitarInspecaoResponse.dadosInspecao, solicitarAutenticacaoConfitecResponse.responseToken.access_token);
            EnviarSolicitacaoInspecaoConfitecResponse enviarSolicitacaoInspecaoConfitecResponse =   await this._enviarSolicitacaoInspecaoConfitecHandler.Handle(enviarSolicitacaoInspecaoConfitecRequest);

            //TODO: 3: Gravar resultado
            GravarRespostaInspecaoRequest gravarRespostaInspecaoRequest = new GravarRespostaInspecaoRequest(enviarSolicitacaoInspecaoConfitecResponse.response);
            GravarRespostaInspecaoResponse gravarRespostaInspecaoResponse = await this._gravarRespostaInspecaoHandler.Handle(gravarRespostaInspecaoRequest);

            //TODO: 4: Retornar resultado

            return new SolicitarInspecaoResponse
            {
                Success = gravarRespostaInspecaoResponse.success,
                Message = $"Sucesso" 
            };
        }
    }
}
