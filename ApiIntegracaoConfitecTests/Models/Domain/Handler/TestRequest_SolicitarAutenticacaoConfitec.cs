using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using NUnit.Framework;

namespace ApiIntegracaoConfitecTests.Models.Domain.Handler
{
    [TestFixture]
    public class TestRequest_SolicitarAutenticacaoConfitec
    {
        [Test]
        public void DadosAutenticacaoComErros()
        {
            //Arrange
            DadosAutenticacao dadosAutenticacao = new() { SompoPassword = "", SompoUsername = "" };

            //Act/Assert
            Assert.Throws(Is.TypeOf<BRQValidationException>()
                          .And.Message.EqualTo("Erro na validação dos dados de Autenticação Confitec"),
                () => new SolicitarAutenticacaoConfitecRequest(dadosAutenticacao));
        }
    }
}
