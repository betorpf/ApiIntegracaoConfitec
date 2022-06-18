using ApiIntegracaoConfitec.Business.Sompo;
using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ApiIntegracaoConfitecTests.Business
{
    [TestFixture]
    public class TestBusiness_Sompo
    {
        public DadosInspecao DadosInspecaoPadrao
        {
            get
            {
                var dadosInspecao = new DadosInspecao()
                {
                    codigoRamo = 1,
                    codigoModalidade = 2,
                    nomeContato = "nomeContato PI: 1",
                    telefoneNumeroContato = "1199999999",
                    codigoCorretorPrincipal = 0,
                    numeroProposta = "1",
                    numeroObjetoSegurado = "numeroObjetoSegurado",
                    numeroApolice = 123,
                    numeroEndosso = 123,
                    codigoUf = "SP",
                    nomeMunicipio = "nomeMunicipio",
                    numeroCep = "00000000",
                    nomeBairro = "nomeBairro",
                    nomeLogradouro = "nomeLogradouro",
                    numeroLogradouro = "321",
                    nomeComplemento = "nomeComplemento",
                    codigoTipoLogradouro = "RUA",
                    codigoTipoPessoa = "F",
                    nomeSegurado = "nomeSegurado",
                    numeroCpfCnpjSegurado = "00000000000000",
                    dataPedidoInspecao = 1012022,
                    observacoes = "observacoes",
                    descricaoObjetoSegurado = "descricaoObjetoSegurado",
                    listaCoberturas = new List<DadosInspecaoCobertura>(),
                    listaContatos = new List<DadosInspecaoContato>(),
                    listaSinistros = new List<DadosInspecaoSinistro>(),
                    listaCamposVariaveis = new List<DadosInspecaoCamposVariaveis>()
                };
                dadosInspecao.listaCoberturas.Add(new DadosInspecaoCobertura()
                {
                    codigoCobertura = 311,
                    valorLmi = 654
                });

                dadosInspecao.listaSinistros.Add(new DadosInspecaoSinistro()
                {
                    numeroSinistro = 654789,
                    causaGeradora = "causaGeradora",
                    dataOcorrencia = "dataOcorrencia",
                    valorSinistro = 654,
                });

                dadosInspecao.listaContatos.Add(new DadosInspecaoContato()
                {
                    nomeContato = "nomeContato1",
                    telefoneNumeroContato = "telefoneNumeroContato1",
                });
                dadosInspecao.listaContatos.Add(new DadosInspecaoContato()
                {
                    nomeContato = "nomeContato2",
                    telefoneNumeroContato = "telefoneNumeroContato2"
                });

                dadosInspecao.listaCamposVariaveis.Add(new DadosInspecaoCamposVariaveis()
                {
                    descricaoCampo = "descricaoCampo",
                    conteudoCampo = "conteudoCampo"
                });

                return dadosInspecao;
            }
        }

        public DadosAutenticacao DadosAutenticacaoPadrao
        {
            get
            {
                return
                    new DadosAutenticacao() { SompoPassword = "pass", SompoUsername = "user" };
            }
        }

        private ResponseToken ResponseTokenPadrao
        {
            get
            {
                return new ResponseToken() { access_token = "asfd" };
            }
        }

        [Test]
        public void SolicitarInspecaoHandler()
        {
            //Arrange
            SolicitarInspecaoRequest solicitarInspecaoRequest = new SolicitarInspecaoRequest() { Num_PI = 1, Num_Local = 1, Tip_Emissao = 1 };

            //Buscar Dados da autentica��o
            var buscarDadosAutenticacaoConfitecResponse = new BuscarDadosAutenticacaoConfitecResponse(this.DadosAutenticacaoPadrao);
            var buscarDadosAutenticacaoConfitecHandler = new Mock<IBuscarDadosAutenticacaoConfitecHandler>();
            buscarDadosAutenticacaoConfitecHandler.Setup(s => s.Handle())
                .ReturnsAsync(buscarDadosAutenticacaoConfitecResponse);

            //Chamar servi�o Confitec de Autentica��o
            var solicitarAutenticacaoConfitecResponse = new SolicitarAutenticacaoConfitecResponse(this.ResponseTokenPadrao);
            var solicitarAutenticacaoConfitecHandler = new Mock<ISolicitarAutenticacaoConfitecHandler>();
            solicitarAutenticacaoConfitecHandler.Setup(s => s.Handle(It.IsAny<SolicitarAutenticacaoConfitecRequest>()))
                .ReturnsAsync(solicitarAutenticacaoConfitecResponse);

            //Buscar Dados para Solicitar a Inspe��o
            BuscarDadosSolicitarInspecaoRequest buscarDadosSolicitarInspecaoRequest = new(1, 1, 1);
            var buscarDadosSolicitarInspecaoHandler = new Mock<IBuscarDadosSolicitarInspecaoHandler>();
            buscarDadosSolicitarInspecaoHandler.Setup(s => s.Handle(It.IsAny<BuscarDadosSolicitarInspecaoRequest>()))
                .ReturnsAsync(new BuscarDadosSolicitarInspecaoResponse(this.DadosInspecaoPadrao));

            //Chamar servi�o Confitec de Enviar Solicita��o de Inspe��o
            EnviarSolicitacaoInspecaoConfitecRequest enviarSolicitacaoInspecaoConfitecRequest = new(this.DadosInspecaoPadrao, this.ResponseTokenPadrao.access_token);
            EnviarSolicitacaoInspecaoConfitecResponse enviarSolicitacaoInspecaoConfitecResponse = new(new ConfitecSolicitarInspecao()
            {
                numeroInspecao = "1",
                dataProcessamento = "20/05/2022",
                codigoResultado = "1",
                mensagemRetorno = "",
                protocoloAbertura = "Sucesso",
                erros = null

            });
            var enviarSolicitacaoInspecaoConfitecHandler = new Mock<IEnviarSolicitacaoInspecaoConfitecHandler>();
            enviarSolicitacaoInspecaoConfitecHandler.Setup(s => s.Handle(It.IsAny<EnviarSolicitacaoInspecaoConfitecRequest>()))
                .ReturnsAsync(enviarSolicitacaoInspecaoConfitecResponse);


            //Gravar resultado
            var gravarRespostaInspecaoHandler = new Mock<IGravarRespostaInspecaoHandler>();
            var gravarRespostaInspecaoRequest = new GravarRespostaInspecaoRequest(solicitarInspecaoRequest, enviarSolicitacaoInspecaoConfitecResponse.response);
            var gravarRespostaInspecaoResponse = new GravarRespostaInspecaoResponse(true, "Sucesso");
            gravarRespostaInspecaoHandler.Setup(s => s.Handle(It.IsAny<GravarRespostaInspecaoRequest>()))
                .ReturnsAsync(gravarRespostaInspecaoResponse);

            //Act
            ISolicitarInspecaoHandler solicitarInspecaoHandler = new SolicitarInspecaoHandler(
                buscarDadosAutenticacaoConfitecHandler.Object,
                solicitarAutenticacaoConfitecHandler.Object,
                buscarDadosSolicitarInspecaoHandler.Object,
                enviarSolicitacaoInspecaoConfitecHandler.Object,
                gravarRespostaInspecaoHandler.Object);
            var result = solicitarInspecaoHandler.Handle(solicitarInspecaoRequest);


            //Assert
            Assert.That(result.Result.Message, Is.EqualTo("Solicita��o de Inspe��o efetuada com sucesso."));
        }

        [Test]
        public void CancelarInspecaoHandler()
        {
            //Arrange
            CancelarInspecaoRequest cancelarInspecaoRequest = new CancelarInspecaoRequest() { Num_PI = 1 };

            //Buscar Dados da autentica��o
            var buscarDadosAutenticacaoConfitecResponse = new BuscarDadosAutenticacaoConfitecResponse(this.DadosAutenticacaoPadrao);
            var buscarDadosAutenticacaoConfitecHandler = new Mock<IBuscarDadosAutenticacaoConfitecHandler>();
            buscarDadosAutenticacaoConfitecHandler.Setup(s => s.Handle())
                .ReturnsAsync(buscarDadosAutenticacaoConfitecResponse);

            //Chamar servi�o Confitec de Autentica��o
            var solicitarAutenticacaoConfitecResponse = new SolicitarAutenticacaoConfitecResponse(this.ResponseTokenPadrao);
            var solicitarAutenticacaoConfitecHandler = new Mock<ISolicitarAutenticacaoConfitecHandler>();
            solicitarAutenticacaoConfitecHandler.Setup(s => s.Handle(It.IsAny<SolicitarAutenticacaoConfitecRequest>()))
                .ReturnsAsync(solicitarAutenticacaoConfitecResponse);


            //Chamar servi�o Confitec de Enviar Cancelamento de Inspe��o
            EnviarSolicitacaoCancelamentoConfitecRequest enviarSolicitacaoCancelamentoConfitecRequest = new(1, this.ResponseTokenPadrao.access_token);
            EnviarSolicitacaoCancelamentoConfitecResponse enviarSolicitacaoCancelamentoConfitecResponse = new(new ResponseCancelarInspecao()
            {
                numeroInspecao = "1",
                dataProcessamento = "20/05/2022",
                codigoResultado = "1",
                mensagemRetorno = "",
                protocoloAbertura = "Sucesso",
                erros = null

            });
            var enviarSolicitacaoCancelamentoConfitecHandler = new Mock<IEnviarSolicitacaoCancelamentoConfitecHandler>();
            enviarSolicitacaoCancelamentoConfitecHandler.Setup(s => s.Handle(It.IsAny<EnviarSolicitacaoCancelamentoConfitecRequest>()))
                .ReturnsAsync(enviarSolicitacaoCancelamentoConfitecResponse);

            //Gravar resultado
            var gravarRespostaCancelamentoHandler = new Mock<IGravarRespostaCancelamentoHandler>();
            var gravarRespostaCancelamentoRequest = new GravarRespostaCancelamentoRequest(enviarSolicitacaoCancelamentoConfitecResponse.response);
            var gravarRespostaCancelamentoResponse = new GravarRespostaCancelamentoResponse(true, "Sucesso");
            gravarRespostaCancelamentoHandler.Setup(s => s.Handle(It.IsAny<GravarRespostaCancelamentoRequest>()))
                .ReturnsAsync(gravarRespostaCancelamentoResponse);

            //Act
            ICancelarInspecaoHandler cancelarInspecaoHandler = new CancelarInspecaoHandler(
                enviarSolicitacaoCancelamentoConfitecHandler.Object,
            buscarDadosAutenticacaoConfitecHandler.Object,
            solicitarAutenticacaoConfitecHandler.Object,
            gravarRespostaCancelamentoHandler.Object);
            var result = cancelarInspecaoHandler.Handle(cancelarInspecaoRequest);


            //Assert
            Assert.That(result.Result.Message, Is.EqualTo("Cancelamento de Inspe��o efetuada com sucesso."));
        }

    }
}
