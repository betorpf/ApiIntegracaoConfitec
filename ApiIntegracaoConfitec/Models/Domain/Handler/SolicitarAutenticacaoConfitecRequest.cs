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
        }
    }
}