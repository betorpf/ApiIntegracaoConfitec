using ApiIntegracaoConfitec.Interfaces.Business.Confitec;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
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

        public async Task<RetornarDadosLaudoResponse> Handle(RetornarDadosLaudoRequest command)
        {
            GravarDadosLaudoResponse informarDadosLaudoResponse = await this._gravarDadosLaudoHandler.Handle(new GravarDadosLaudoRequest() { pi = command.PI });

            return new RetornarDadosLaudoResponse
            {
                NumPI = command.PI
            };

        }
    }
}
