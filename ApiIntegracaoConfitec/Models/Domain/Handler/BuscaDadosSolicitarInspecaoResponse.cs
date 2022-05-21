using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscaDadosSolicitarInspecaoResponse
    {
        public DadosInspecao dadosInspecao;
        public bool Success { get; set; }
        public string Message { get; set; }

        public BuscaDadosSolicitarInspecaoResponse(DadosInspecao dadosInspecao)
        {
            this.dadosInspecao = dadosInspecao;
            if (this.dadosInspecao != null)
                this.Success = true;
        }

    }
}
