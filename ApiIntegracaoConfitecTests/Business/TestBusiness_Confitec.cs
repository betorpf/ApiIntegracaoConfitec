using ApiIntegracaoConfitec.Business.Confitec;
using ApiIntegracaoConfitec.Interfaces.Business.Confitec;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ApiIntegracaoConfitecTests.Business
{
    [TestFixture]
    class TestBusiness_Confitec
    {
        private ResultadoInspecao resultadoInspecaoPadrao
        {
            get
            {
                return new ResultadoInspecao()
                {
                    numeroOrcamento = 1,
                    numeroVersaoOrcamento = 1,
                    numeroItemVersaoOrcamento = 1,
                    numeroCnpjCpf = 1,
                    numeroSolicitacaoInspecao = "1",
                    flagLocalRiscoEnderecoInspecao = "1",
                    nomeContato = "nomeContato",
                    numeroTelefoneContato = "numeroTelefoneContato",
                    codigoTipoLogradouro = 1,
                    codigoLogradouro = 1,
                    nomeLogradouro = "nomeLogradouro",
                    numeroLogradouro = 1,
                    numeroCep = 1,
                    nomeComplemento = "nomeComplemento",
                    nomeBairro = "nomeBairro",
                    nomeCidade = "nomeCidade",
                    codigoUnidadeFederacao = 1,
                    codigoPais = 1,
                    textoPontoReferencia = "textoPontoReferencia",
                    numeroLatitude = 1,
                    numeroLongitude = 1,
                    codigoStatusSolicitacaoInspecao = "codigoStatusSolicitacaoInspecao",
                    codigoStatusParecerSolicitacaoInspecao = "codigoStatusParecerSolicitacaoInspecao",
                    flagSolicitacaoInspecaoAutomatico = "flagSolicitacaoInspecaoAutomatico",
                    dataSolicitacaoInspecao = DateTime.Now,
                    flagInspetorConfiavel = "flagInspetorConfiavel",
                    codigoAtividade = "codigoAtividade",
                    flagCodigoAtividadeAlterada = "flagCodigoAtividadeAlterada",
                    flagEnderecoAlterado = "flagEnderecoAlterado",
                    flagAtivo = "flagAtivo",
                    listaCoberturas = new List<ResultadoInspecaoCobertura>() { new ResultadoInspecaoCobertura() { codigoCobertura = "1", descricaoParecer = "descricaoParecer", tipoParecer = "tipoParecer" } },
                    motivoInspecao = 1,
                    dataAgendamento = DateTime.Now,
                    numeroSinistro = 1,
                };
            }
        }

        [Test]
        public void GravarDadosLaudoHandler()
        {
            //TODO: Validar
            //Arrange
            var resultadoInspecaoRequest = new ResultadoInspecaoRequest() { ResultadoInspecao = this.resultadoInspecaoPadrao };
            var gravarDadosLaudoRequest = new GravarDadosLaudoRequest(this.resultadoInspecaoPadrao);

            //Gravar resultado
            var gravarDadosLaudoHandler = new Mock<IGravarDadosLaudoHandler>();
            var gravarDadosLaudoResponse = new GravarDadosLaudoResponse(true, "Sucesso");

            gravarDadosLaudoHandler.Setup(s => s.Handle(It.IsAny<GravarDadosLaudoRequest>()))
                .ReturnsAsync(gravarDadosLaudoResponse);

            //Act
            IEnviarRetornoLaudoHandler enviarRetornoLaudoHandler = new EnviarRetornoLaudoHandler(
                gravarDadosLaudoHandler.Object);
            var result = enviarRetornoLaudoHandler.Handle(resultadoInspecaoRequest);

            //Assert
            Assert.That(result.Result.Message, Is.EqualTo("Retorno de Laudo recebido com sucesso"));
        }
    }
}
