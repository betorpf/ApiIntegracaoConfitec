using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaCancelamentoRequest
    {
        public ResponseCancelarInspecao responseCancelarInspecao { get; set; }

        public GravarRespostaCancelamentoRequest(ResponseCancelarInspecao responseCancelarInspecao)
        {
            this.responseCancelarInspecao = responseCancelarInspecao;
            var ListaValidacao = ValidationUtility.ListValidateObject(this.responseCancelarInspecao);
            if (this.responseCancelarInspecao.erros != null && this.responseCancelarInspecao.erros.Count > 0)
            {
                throw new ConfitecErrorsException("Validação de dados retornados da Confitec", this.responseCancelarInspecao.erros);
            }
        }
    }
}
