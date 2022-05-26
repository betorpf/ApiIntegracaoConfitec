using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaCancelamentoRequest
    {
        public ResponseCancelarInspecao responseCancelarInspecao { get; set; }

        public GravarRespostaCancelamentoRequest(ResponseCancelarInspecao responseCancelarInspecao)
        {
            this.responseCancelarInspecao = responseCancelarInspecao;
        }
    }
}
