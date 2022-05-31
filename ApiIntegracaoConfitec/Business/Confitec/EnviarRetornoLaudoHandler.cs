using ApiIntegracaoConfitec.Interfaces.Business.Confitec;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Confitec.Controller;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Business.Confitec
{
    public class EnviarRetornoLaudoHandler : IEnviarRetornoLaudoHandler
    {
        private readonly IGravarDadosLaudoHandler _gravarDadosLaudoHandler;

        public EnviarRetornoLaudoHandler(
            IGravarDadosLaudoHandler gravarDadosLaudoHandler = null)
        {
            this._gravarDadosLaudoHandler = gravarDadosLaudoHandler;
        }

        public async Task<RetornarDadosLaudoResponse> Handle(ResultadoInspecaoRequest command)
        {
            gravarDadosLaudoRequest gravarDadosLaudoRequest = new(command);
            GravarDadosLaudoResponse informarDadosLaudoResponse = await this._gravarDadosLaudoHandler.Handle(gravarDadosLaudoRequest.resultadoInspecaoRequest);

            return new RetornarDadosLaudoResponse
            {
                Success = true,
                Message = "Retorno de Laudo recebido com sucesso",
                Errors = null
            };

        }
    }
}
