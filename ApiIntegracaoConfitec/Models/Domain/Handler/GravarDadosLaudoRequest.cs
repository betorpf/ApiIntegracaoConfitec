using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Confitec.Controller;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarDadosLaudoRequest
    {
        public ResultadoInspecao resultadoInspecao { get; set; }

        public GravarDadosLaudoRequest(ResultadoInspecao resultadoInspecao)
        {
            this.resultadoInspecao = resultadoInspecao;
        }
    }
}
