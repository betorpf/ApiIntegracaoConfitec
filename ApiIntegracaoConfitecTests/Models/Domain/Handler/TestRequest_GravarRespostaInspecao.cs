using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using NUnit.Framework;
using System.Collections.Generic;

namespace ApiIntegracaoConfitecTests.Models.Domain.Handler
{
    [TestFixture]
    public class TestRequest_GravarRespostaInspecao
    {
        [Test]
        public void SolicitacaoInspecaoComErros()
        {
            //Arrange
            var listaErro = new List<ErroConfitec>();
            listaErro.Add(new ErroConfitec() { CodigoErro = "1", DescricaoErro = "Erro Genérico" });
            SolicitarInspecaoRequest solicitarInspecaoRequest = new() { Num_PI = 1, Num_Local =  1, Tip_Emissao = 1 };

            //Act
            EnviarSolicitacaoInspecaoConfitecResponse enviarSolicitacaoInspecaoConfitecResponse = new(new ConfitecSolicitarInspecao()
            {
                numeroInspecao = "1",
                dataProcessamento = "20/05/2022",
                codigoResultado = "1",
                mensagemRetorno = "",
                protocoloAbertura = "Falha",
                erros = listaErro
            });

            //Assert
            Assert.Throws(Is.TypeOf<ConfitecErrorsException>()
                          .And.Message.EqualTo("Validação de dados retornados da Confitec"),
                () => new GravarRespostaInspecaoRequest(solicitarInspecaoRequest, enviarSolicitacaoInspecaoConfitecResponse.response));
        }
    }
}
