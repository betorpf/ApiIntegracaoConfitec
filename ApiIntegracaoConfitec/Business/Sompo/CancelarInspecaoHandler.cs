using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Business.Sompo
{
    public class CancelarInspecaoHandler : ICancelarInspecaoHandler
    {
        private readonly IEnviarSolicitacaoCancelamentoConfitecHandler _enviarSolicitacaoCancelamentoConfitecHandler;
        private readonly IBuscarDadosAutenticacaoConfitecHandler _buscarDadosAutenticacaoConfitecHandler;
        private readonly ISolicitarAutenticacaoConfitecHandler _solicitarAutenticacaoConfitecHandler;
        private readonly IGravarRespostaCancelamentoHandler _gravarRespostaCancelamentoHandler;

        public CancelarInspecaoHandler(
            IEnviarSolicitacaoCancelamentoConfitecHandler enviarSolicitacaoCancelamentoConfitecHandler = null, 
            IBuscarDadosAutenticacaoConfitecHandler buscarDadosAutenticacaoConfitecHandler = null, 
            ISolicitarAutenticacaoConfitecHandler solicitarAutenticacaoConfitecHandler = null, 
            IGravarRespostaCancelamentoHandler gravarRespostaCancelamentoHandler = null)
        {
            this._enviarSolicitacaoCancelamentoConfitecHandler = enviarSolicitacaoCancelamentoConfitecHandler;
            this._buscarDadosAutenticacaoConfitecHandler = buscarDadosAutenticacaoConfitecHandler;
            this._solicitarAutenticacaoConfitecHandler = solicitarAutenticacaoConfitecHandler;
            this._gravarRespostaCancelamentoHandler = gravarRespostaCancelamentoHandler;
        }

        public async Task<CancelarInspecaoHttpResponse> Handle(CancelarInspecaoRequest cancelarInspecaoRequest)
        {
            //Cria Response com o NumPI Informado
            CancelarInspecaoHttpResponse cancelarInspecaoResponse = new CancelarInspecaoHttpResponse(cancelarInspecaoRequest.PI);

            //Buscar Dados da autenticação
            BuscarDadosAutenticacaoConfitecResponse buscarDadosAutenticacaoConfitecResponse = await this._buscarDadosAutenticacaoConfitecHandler.Handle();

            //Chamar serviço Confitec de Autenticação
            SolicitarAutenticacaoConfitecRequest solicitarAutenticacaoConfitecRequest = new SolicitarAutenticacaoConfitecRequest(buscarDadosAutenticacaoConfitecResponse.dadosAutenticacao);
            SolicitarAutenticacaoConfitecResponse solicitarAutenticacaoConfitecResponse = await this._solicitarAutenticacaoConfitecHandler.Handle(solicitarAutenticacaoConfitecRequest);

            //Chamar serviço Confitec de Enviar Cancelamento de Inspeção
            EnviarSolicitacaoCancelamentoConfitecRequest enviarSolicitacaoCancelamentoConfitecRequest = new EnviarSolicitacaoCancelamentoConfitecRequest(cancelarInspecaoResponse.NumPI, solicitarAutenticacaoConfitecResponse.responseToken.access_token);
            EnviarSolicitacaoCancelamentoConfitecResponse enviarSolicitacaoCancelamentoConfitecResponse = await this._enviarSolicitacaoCancelamentoConfitecHandler.Handle(enviarSolicitacaoCancelamentoConfitecRequest);

            //TODO: 3: Gravar resultado
            GravarRespostaCancelamentoRequest gravarRespostaCancelamentoRequest = new GravarRespostaCancelamentoRequest(enviarSolicitacaoCancelamentoConfitecResponse.response);
            GravarRespostaCancelamentoResponse gravarRespostaCancelamentoResponse = await this._gravarRespostaCancelamentoHandler.Handle(gravarRespostaCancelamentoRequest);

            //TODO: 4: Retornar resultado
            cancelarInspecaoResponse.Success = gravarRespostaCancelamentoResponse.Success;
            cancelarInspecaoResponse.Message = gravarRespostaCancelamentoResponse.Message;
            return cancelarInspecaoResponse;
        }
    }
}
