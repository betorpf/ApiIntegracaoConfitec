using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscarDadosSolicitarInspecaoResponse
    {
        public DadosInspecao dadosInspecao { get; set; }

        public BuscarDadosSolicitarInspecaoResponse(DadosInspecao dadosInspecao)
        {
            this.dadosInspecao = dadosInspecao;

            if(this.dadosInspecao != null && this.dadosInspecao.resultadoBusca != null)
            {
                if (this.dadosInspecao.resultadoBusca.solicitarInspecao)
                {
                    return;
                }
                throw new InspecaoException(this.dadosInspecao.resultadoBusca.ToString());
            }
        }
    }
}
