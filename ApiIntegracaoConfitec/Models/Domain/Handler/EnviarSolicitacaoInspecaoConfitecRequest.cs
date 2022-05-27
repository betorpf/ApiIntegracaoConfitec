﻿using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class EnviarSolicitacaoInspecaoConfitecRequest
    {
        public DadosInspecao dadosInspecao;
        public string access_token;
        public int PI { get; set; }
        public string Codigo { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public EnviarSolicitacaoInspecaoConfitecRequest(DadosInspecao dadosInspecao, string access_token)
        {
            this.dadosInspecao = dadosInspecao;
            this.access_token = access_token;

            var ListaValidacao = ValidationUtility.ListValidateObject(this.dadosInspecao);
            if (ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Validação de dados da inspeção selecionada.", ListaValidacao);
            }

        }
    }
}
