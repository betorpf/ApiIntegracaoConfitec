using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscarDadosSolicitarInspecaoResponse
    {
        public DadosInspecao dadosInspecao;

        public BuscarDadosSolicitarInspecaoResponse(DadosInspecao dadosInspecao)
        {
            this.dadosInspecao = dadosInspecao;
        }
    }
}
