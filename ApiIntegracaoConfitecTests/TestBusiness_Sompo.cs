using ApiIntegracaoConfitec.Business.Sompo;
using ApiIntegracaoConfitec.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Service;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using ApiIntegracaoConfitec.Models.Sompo.Controller;
using Moq;
using NUnit.Framework;

namespace ApiIntegracaoConfitecTests
{
    [TestFixture]
    public class TestBusiness_Sompo
    {
        public DadosInspecao DadosInspecaoPadrao
        {
            get
            {
                return new DadosInspecao()
                {
                    codigoRamo = 1,
                    codigoModalidade = 2,
                    nomeContato = "nomeContato PI: 1"
,
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
                    codigoCobertura = 311,
                    valorLmi = 654,
                    nomeContato1 = "nomeContato1",
                    telefoneNumeroContato1 = "telefoneNumeroContato1",
                    nomeContato2 = "nomeContato2",
                    telefoneNumeroContato2 = "telefoneNumeroContato2",
                    numeroSinistro = 654789,
                    causaGeradora = "causaGeradora",
                    dataOcorrencia = "dataOcorrencia",
                    valorSinistro = 654,
                    descricaoCampo = "descricaoCampo",
                    conteudoCampo = "conteudoCampo"
                };
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
            SolicitarInspecaoRequest solicitarInspecaoRequest = new SolicitarInspecaoRequest() { PI = 1 };

            SolicitarAutenticacaoConfitecRequest solicitarAutenticacaoConfitecRequest = new(this.DadosAutenticacaoPadrao);

            //Buscar Dados da autenticação
            var buscarDadosAutenticacaoConfitecResponse = new BuscarDadosAutenticacaoConfitecResponse(this.DadosAutenticacaoPadrao);
            var buscarDadosAutenticacaoConfitecHandler = new Mock<IBuscarDadosAutenticacaoConfitecHandler>();
            buscarDadosAutenticacaoConfitecHandler.Setup(s => s.Handle())
                .ReturnsAsync(buscarDadosAutenticacaoConfitecResponse);

            //Chamar serviço Confitec de Autenticação
            var solicitarAutenticacaoConfitecResponse = new SolicitarAutenticacaoConfitecResponse(this.ResponseTokenPadrao);
            var solicitarAutenticacaoConfitecHandler = new Mock<ISolicitarAutenticacaoConfitecHandler>();
            solicitarAutenticacaoConfitecHandler.Setup(s => s.Handle(It.IsAny<SolicitarAutenticacaoConfitecRequest>()))
                .ReturnsAsync(solicitarAutenticacaoConfitecResponse);

            //Buscar Dados para Solicitar a Inspeção
            BuscarDadosSolicitarInspecaoRequest buscarDadosSolicitarInspecaoRequest = new(1);
            var buscarDadosSolicitarInspecaoHandler = new Mock<IBuscarDadosSolicitarInspecaoHandler>();
            buscarDadosSolicitarInspecaoHandler.Setup(s => s.Handle(It.IsAny<BuscarDadosSolicitarInspecaoRequest>()))
                .ReturnsAsync(new BuscarDadosSolicitarInspecaoResponse(this.DadosInspecaoPadrao));

            //Chamar serviço Confitec de Enviar Solicitação de Inspeção
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
            var gravarRespostaInspecaoRequest = new GravarRespostaInspecaoRequest(enviarSolicitacaoInspecaoConfitecResponse.response);
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
            Assert.That(result.Result.Message, Is.EqualTo("Solicitação de Inspeção efetuada com sucesso."));
        }
    }
}
