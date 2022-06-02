using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitecTests.Models.Domain.Handler
{
    [TestFixture]
    public class TestRequest_SolicitacaoInspecao
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

        [Test]
        public void Request_SolicitacaoInspecao()
        {
            //Arrange
            var request = new RequestSolicitacaoInspecao(this.DadosInspecaoPadrao);
            var errorcount = ValidationUtility.ListValidateObject(request).Count;
            Assert.AreEqual(0, errorcount);
        }
    }
}
