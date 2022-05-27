using ApiIntegracaoConfitec.Domain.Handler;
using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Entity;
using System;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscarDadosAutenticacaoConfitecResponse
    {
        public DadosAutenticacao dadosAutenticacao;
        public bool Success { get; set; }
        public string Message { get; set; }

        public BuscarDadosAutenticacaoConfitecResponse(DadosAutenticacao dadosAutenticacao)
        {

            this.dadosAutenticacao = dadosAutenticacao;
            this.Message = ValidationUtility.ValidateObject(this.dadosAutenticacao);
            this.Success = true;

            //if (!string.IsNullOrEmpty(this.Message))
            //    throw new System.Exception(message: this.Message);


            var ListaValidacao = ValidationUtility.ListValidateObject(this.dadosAutenticacao);
            if(ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Erro na validação dos dados de Autenticação Confitec", ListaValidacao);
            }
        }
    }
}
