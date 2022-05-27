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

        public BuscarDadosAutenticacaoConfitecResponse(DadosAutenticacao dadosAutenticacao)
        {
            this.dadosAutenticacao = dadosAutenticacao;
        }
    }
}
