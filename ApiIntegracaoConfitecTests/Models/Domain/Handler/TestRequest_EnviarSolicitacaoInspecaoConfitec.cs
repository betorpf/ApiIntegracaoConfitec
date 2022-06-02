using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using NUnit.Framework;
using System.Collections.Generic;

namespace ApiIntegracaoConfitecTests.Models.Domain.Handler
{
    [TestFixture]
    public class TestRequest_EnviarSolicitacaoInspecaoConfitec
    {
        [Test]
        public void DadosInspecaoComErros()
        {
            //Arrange
            DadosInspecao dadosInspecao = new()
            {
                codigoRamo = 1,
                codigoModalidade = 2,
                nomeContato = "",
                telefoneNumeroContato = "",
                codigoCorretorPrincipal = 0,
                numeroProposta = "",
                numeroObjetoSegurado = "",
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

            string access_token = "321";

            //Act/Assert
            Assert.Throws(Is.TypeOf<BRQValidationException>()
                          .And.Message.EqualTo("Validação de dados da inspeção selecionada."),
                () => new EnviarSolicitacaoInspecaoConfitecRequest(dadosInspecao,access_token));
        }

        [Test]
        public void AccessTokenComErros()
        {
            //Arrange
            DadosInspecao dadosInspecao = new()
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

            string access_token = null;

            //Act/Assert
            Assert.Throws(Is.TypeOf<BRQValidationException>()
                          .And.Message.EqualTo("Access Token vazio."),
                () => new EnviarSolicitacaoInspecaoConfitecRequest(dadosInspecao, access_token));
        }

    }

}
