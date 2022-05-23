using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscarDadosSolicitarInspecaoResponse
    {
        public DadosInspecao dadosInspecao;
        public bool Success { get; set; }
        public string Message { get; set; }

        public BuscarDadosSolicitarInspecaoResponse(DadosInspecao dadosInspecao)
        {
            this.dadosInspecao = dadosInspecao;
            if (this.dadosInspecao != null)
                this.Success = true;
        }

    }
}
