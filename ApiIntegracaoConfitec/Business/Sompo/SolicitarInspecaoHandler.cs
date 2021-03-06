using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System.Collections.Generic;
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
                IBuscarDadosAutenticacaoConfitecHandler buscarDadosAutenticacaoConfitecHandler = null,
                ISolicitarAutenticacaoConfitecHandler solicitarAutenticacaoConfitecHandler = null,
                IBuscarDadosSolicitarInspecaoHandler buscarDadosSolicitarInspecaoHandler = null,
                IEnviarSolicitacaoInspecaoConfitecHandler enviarSolicitacaoInspecaoConfitecHandler = null, 
                IGravarRespostaInspecaoHandler gravarRespostaInspecaoHandler = null)
        {
            this._buscarDadosAutenticacaoConfitecHandler = buscarDadosAutenticacaoConfitecHandler;
            this._solicitarAutenticacaoConfitecHandler = solicitarAutenticacaoConfitecHandler;

            this._buscarDadosSolicitarInspecaoHandler = buscarDadosSolicitarInspecaoHandler;
            this._enviarSolicitacaoInspecaoConfitecHandler = enviarSolicitacaoInspecaoConfitecHandler;
            
            
            this._gravarRespostaInspecaoHandler = gravarRespostaInspecaoHandler;
        }

        public async Task<SolicitarInspecaoHttpResponse> Handle(SolicitarInspecaoRequest solicitarInspecaoRequest)
        {
            //Buscar Dados para Solicitar a Inspeção
            BuscarDadosSolicitarInspecaoRequest buscarDadosSolicitarInspecaoRequest = new(solicitarInspecaoRequest.Num_PI, solicitarInspecaoRequest.Num_Local, solicitarInspecaoRequest.Tip_Emissao);
            BuscarDadosSolicitarInspecaoResponse buscaDadosSolicitarInspecaoResponse = await this._buscarDadosSolicitarInspecaoHandler.Handle(buscarDadosSolicitarInspecaoRequest);

            //Buscar Dados da autenticação
            BuscarDadosAutenticacaoConfitecResponse buscarDadosAutenticacaoConfitecResponse = await this._buscarDadosAutenticacaoConfitecHandler.Handle();

            //Chamar serviço Confitec de Autenticação
            SolicitarAutenticacaoConfitecRequest solicitarAutenticacaoConfitecRequest = new(buscarDadosAutenticacaoConfitecResponse.dadosAutenticacao);
            SolicitarAutenticacaoConfitecResponse solicitarAutenticacaoConfitecResponse = await this._solicitarAutenticacaoConfitecHandler.Handle(solicitarAutenticacaoConfitecRequest);

            //Chamar serviço Confitec de Enviar Solicitação de Inspeção
            EnviarSolicitacaoInspecaoConfitecRequest enviarSolicitacaoInspecaoConfitecRequest = new(buscaDadosSolicitarInspecaoResponse.dadosInspecao, solicitarAutenticacaoConfitecResponse.responseToken.access_token);
            EnviarSolicitacaoInspecaoConfitecResponse enviarSolicitacaoInspecaoConfitecResponse = await this._enviarSolicitacaoInspecaoConfitecHandler.Handle(enviarSolicitacaoInspecaoConfitecRequest);

            //Gravar resultado
            GravarRespostaInspecaoRequest gravarRespostaInspecaoRequest = new(solicitarInspecaoRequest, enviarSolicitacaoInspecaoConfitecResponse.response);
            GravarRespostaInspecaoResponse gravarRespostaInspecaoResponse = await this._gravarRespostaInspecaoHandler.Handle(gravarRespostaInspecaoRequest);

            //Retornar resultado
            SolicitarInspecaoHttpResponse solicitarInspecaoResponse = new() {
                Success = true,
                Message = "Solicitação de Inspeção efetuada com sucesso.",
            };

            return solicitarInspecaoResponse;
        }
    }
}
