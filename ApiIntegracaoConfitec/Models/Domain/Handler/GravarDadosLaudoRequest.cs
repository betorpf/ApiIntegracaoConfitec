using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Confitec.Controller;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class gravarDadosLaudoRequest
    {
        public ResultadoInspecaoRequest resultadoInspecaoRequest { get; set; }

        public gravarDadosLaudoRequest(ResultadoInspecaoRequest resultadoInspecaoRequest)
        {
            this.resultadoInspecaoRequest = resultadoInspecaoRequest;
        }
    }
}
