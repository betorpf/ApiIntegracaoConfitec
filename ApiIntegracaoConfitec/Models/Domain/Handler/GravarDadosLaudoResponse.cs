using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarDadosLaudoResponse
    {
        public DadosLaudo dadosLaudo;

        public GravarDadosLaudoResponse(DadosLaudo dadosLaudo)
        {
            this.dadosLaudo = dadosLaudo;
            var ListaValidacao = ValidationUtility.ListValidateObject(this.dadosLaudo);
            if (ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Validação nos dados do Laudo", ListaValidacao);
            }
        }
    }
}
