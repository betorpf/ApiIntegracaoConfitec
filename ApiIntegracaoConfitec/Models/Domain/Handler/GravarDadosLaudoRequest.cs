using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Confitec.Controller;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class gravarDadosLaudoRequest
    {
        public ResultadoInspecao resultadoInspecao { get; set; }

        public gravarDadosLaudoRequest(ResultadoInspecao resultadoInspecao)
        {
            this.resultadoInspecao = resultadoInspecao;
        }
    }
}
