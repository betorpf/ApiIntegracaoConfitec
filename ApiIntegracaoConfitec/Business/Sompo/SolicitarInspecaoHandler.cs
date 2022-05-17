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
        IBuscarDadosSolicitarInspecaoHandler _buscarDadosSolicitarInspecaoHandler;
        public SolicitarInspecaoHandler(IBuscarDadosSolicitarInspecaoHandler buscarDadosSolicitarInspecaoHandler = null)
        {
            this._buscarDadosSolicitarInspecaoHandler = buscarDadosSolicitarInspecaoHandler;
        }

        public async Task<SolicitarInspecaoResponse> Handle(SolicitarInspecaoRequest command)
        {

            //TODO: 1: Consultar dados da inspeção no banco de dados
            BuscaDadosSolicitarInspecaoResponse response = await this._buscarDadosSolicitarInspecaoHandler.Handle(new BuscaDadosSolicitarInspecaoRequest() { pi = command.PI });

            //TODO: 1.1: Validar se as informações estão certas

            //TODO: 2: Chamar serviço Confitec
            //this._enviarSolicitacaoInspecaoHandle(response);

            //TODO: 3: Gravar resultado
            //this.

            //TODO: 4: Retornar resultado

            return new SolicitarInspecaoResponse
            {
                Success = true,
                Message = $"Sucesso {response.Codigo}" 
            };
        }
    }
}
