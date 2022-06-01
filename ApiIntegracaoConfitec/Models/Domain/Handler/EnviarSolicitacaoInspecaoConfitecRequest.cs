using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoInspecaoConfitecRequest
    {
        public DadosInspecao dadosInspecao;
        public string access_token;

        public EnviarSolicitacaoInspecaoConfitecRequest(DadosInspecao dadosInspecao, string access_token)
        {
            this.dadosInspecao = dadosInspecao;
            this.access_token = access_token;

            var ListaValidacao = ValidationUtility.ListValidateObject(this.dadosInspecao);
            if (ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Validação de dados da inspeção selecionada.", ListaValidacao);
            }
            if(string.IsNullOrEmpty(access_token))
            {
                throw new BRQValidationException("Access Token vazio.");
            }

        }
    }
}
