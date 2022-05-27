using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class SolicitarAutenticacaoConfitecRequest
    {
        public DadosAutenticacao dadosAutenticacao;

        public SolicitarAutenticacaoConfitecRequest(DadosAutenticacao dadosAutenticacao)
        {
            this.dadosAutenticacao = dadosAutenticacao;
            var ListaValidacao = ValidationUtility.ListValidateObject(this.dadosAutenticacao);
            if (ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Erro na validação dos dados de Autenticação Confitec", ListaValidacao);
            }
        }
    }
}