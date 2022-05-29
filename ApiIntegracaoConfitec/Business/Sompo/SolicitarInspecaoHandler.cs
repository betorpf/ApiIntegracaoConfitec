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

        public async Task<SolicitarInspecaoHttpResponse> Handle(SolicitarInspecaoRequest solicitarInspecaoRequest)
        {
            //Cria Response com o NumPI Informado
            SolicitarInspecaoHttpResponse solicitarInspecaoResponse =  new();
            
            //Buscar Dados da autenticação
            BuscarDadosAutenticacaoConfitecResponse buscarDadosAutenticacaoConfitecResponse = await this._buscarDadosAutenticacaoConfitecHandler.Handle();

            //Buscar Dados para Solicitar a Inspeção
            BuscarDadosSolicitarInspecaoRequest buscarDadosSolicitarInspecaoRequest = new(solicitarInspecaoRequest.PI);
            BuscarDadosSolicitarInspecaoResponse buscaDadosSolicitarInspecaoResponse = await this._buscarDadosSolicitarInspecaoHandler.Handle(buscarDadosSolicitarInspecaoRequest);

            //Chamar serviço Confitec de Autenticação
            SolicitarAutenticacaoConfitecRequest solicitarAutenticacaoConfitecRequest = new(buscarDadosAutenticacaoConfitecResponse.dadosAutenticacao);
            SolicitarAutenticacaoConfitecResponse solicitarAutenticacaoConfitecResponse = await this._solicitarAutenticacaoConfitecHandler.Handle(solicitarAutenticacaoConfitecRequest);

            //Chamar serviço Confitec de Enviar Solicitação de Inspeção
            EnviarSolicitacaoInspecaoConfitecRequest enviarSolicitacaoInspecaoConfitecRequest = new(buscaDadosSolicitarInspecaoResponse.dadosInspecao, solicitarAutenticacaoConfitecResponse.responseToken.access_token);
            EnviarSolicitacaoInspecaoConfitecResponse enviarSolicitacaoInspecaoConfitecResponse =   await this._enviarSolicitacaoInspecaoConfitecHandler.Handle(enviarSolicitacaoInspecaoConfitecRequest);

            //TODO: 3: Gravar resultado
            GravarRespostaInspecaoRequest gravarRespostaInspecaoRequest = new(enviarSolicitacaoInspecaoConfitecResponse.response);
            GravarRespostaInspecaoResponse gravarRespostaInspecaoResponse = await this._gravarRespostaInspecaoHandler.Handle(gravarRespostaInspecaoRequest);

            //TODO: 4: Retornar resultado
            solicitarInspecaoResponse.Success = true;
            solicitarInspecaoResponse.Message = "Solicitação de Inspeção efetuada com sucesso.";
            return solicitarInspecaoResponse;
        }
    }
}
