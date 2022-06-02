using ApiIntegracaoConfitec.Interfaces.Services;
using ApiIntegracaoConfitec.Models.Entity;
using System.Collections.Generic;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class RequestSolicitacaoInspecao : IRequest
    {
        public string matriculaUsuario { get; set; }
        public string codigoEmpresaGrupoSegurador { get; set; }
        public string codigoSucursal { get; set; }
        public string codigoRamo { get; set; }
        public string codigoCategoria { get; set; }
        public string codigoModalidade { get; set; }
        public string exigencia { get; set; }
        public string nomeContato { get; set; }
        public string telefoneDDIContato { get; set; }
        public string telefoneDDDContato { get; set; }
        public string telefoneRamalContato { get; set; }
        public string telefoneNumeroContato { get; set; }
        public string telefoneTipoContato { get; set; }
        public string codigoCorretorPrincipal { get; set; }
        public string codigoTipoSeguro { get; set; }
        public string numeroOrcamento { get; set; }
        public string numeroVersaoOrcamento { get; set; }
        public string numeroProposta { get; set; }
        public string numeroVersaoProposta { get; set; }
        public string numeroObjetoSegurado { get; set; }
        public string numeroApolice { get; set; }
        public string numeroEndosso { get; set; }
        public string descricaoOferta { get; set; }
        public string codigoPais { get; set; }
        public string codigoUf { get; set; }
        public string nomeMunicipio { get; set; }
        public string numeroCep { get; set; }
        public string nomeBairro { get; set; }
        public string nomeLogradouro { get; set; }
        public string numeroLogradouro { get; set; }
        public string nomeComplemento { get; set; }
        public string nomePontoReferencia { get; set; }
        public string codigoTipoLogradouro { get; set; }
        public string codigoTipoPessoa { get; set; }
        public string nomeSegurado { get; set; }
        public string numeroCpfCnpjSegurado { get; set; }
        public string dataPedidoInspecao { get; set; }
        public string dataBase { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string premioItem { get; set; }
        public string observacoes { get; set; }
        public string descricaoObjetoSegurado { get; set; }
        public string dataPedidoOriginal { get; set; }
        public string sistemaOrigem { get; set; }
        public List<CoberturaInspecao> listaCoberturaInspecao { get; set; }
        public List<TelefoneContato> listaTelefoneContato { get; set; }
        public List<Sinistro> listaSinistro { get; set; }
        public List<CamposVariaveis> listaCamposVariaveis { get; set; }

        public RequestSolicitacaoInspecao(DadosInspecao dadosInspecao)
        {
            this.matriculaUsuario = null;
            this.codigoEmpresaGrupoSegurador = null;
            this.codigoSucursal = null;
            this.codigoRamo = dadosInspecao.codigoRamo.ToString();
            this.codigoCategoria = null;
            this.codigoModalidade = dadosInspecao.codigoModalidade.ToString();
            this.exigencia = null;
            this.nomeContato = dadosInspecao.nomeContato;
            this.telefoneDDIContato = null;
            this.telefoneDDDContato = null;
            this.telefoneRamalContato = null;
            this.telefoneNumeroContato = dadosInspecao.telefoneNumeroContato;
            this.telefoneTipoContato = null;
            this.codigoCorretorPrincipal = dadosInspecao.codigoCorretorPrincipal.ToString();
            this.codigoTipoSeguro = null;
            this.numeroOrcamento = null;
            this.numeroVersaoOrcamento = null;
            this.numeroProposta = dadosInspecao.numeroProposta;
            this.numeroVersaoProposta = null;
            this.numeroObjetoSegurado = dadosInspecao.numeroObjetoSegurado;
            this.numeroApolice = dadosInspecao.numeroApolice.ToString();
            this.numeroEndosso = dadosInspecao.numeroEndosso.ToString();
            this.descricaoOferta = null;
            this.codigoPais = null;
            this.codigoUf = dadosInspecao.codigoUf;
            this.nomeMunicipio = dadosInspecao.nomeMunicipio;
            this.numeroCep = dadosInspecao.numeroCep;
            this.nomeBairro = dadosInspecao.nomeBairro;
            this.nomeLogradouro = dadosInspecao.nomeLogradouro;
            this.numeroLogradouro = dadosInspecao.numeroLogradouro;
            this.nomeComplemento = dadosInspecao.nomeComplemento;
            this.nomePontoReferencia = null;
            this.codigoTipoLogradouro = dadosInspecao.codigoTipoLogradouro;
            this.codigoTipoPessoa = dadosInspecao.codigoTipoPessoa;
            this.nomeSegurado = dadosInspecao.nomeSegurado;
            this.numeroCpfCnpjSegurado = dadosInspecao.numeroCpfCnpjSegurado;
            this.dataPedidoInspecao = dadosInspecao.dataPedidoInspecao.ToString();
            this.dataBase = null;
            this.latitude = null;
            this.longitude = null;
            this.premioItem = null;
            this.observacoes = dadosInspecao.observacoes;
            this.descricaoObjetoSegurado = dadosInspecao.descricaoObjetoSegurado;
            this.dataPedidoOriginal = null;
            this.sistemaOrigem = null;

            this.listaCoberturaInspecao = new List<CoberturaInspecao>();
            foreach (var c in dadosInspecao.listaCoberturas)
            {
                this.listaCoberturaInspecao.Add(new CoberturaInspecao()
                {
                    codigoCobertura = c.codigoCobertura.ToString(),
                    valorLmi = c.valorLmi.ToString(),
                    coberturaBasica = null
                });
            }

            this.listaTelefoneContato = new List<TelefoneContato>();
            foreach (var c in dadosInspecao.listaContatos)
            {
                this.listaTelefoneContato.Add(new TelefoneContato()
                {
                    nomeContato = c.nomeContato,
                    telefoneDDIContato = null,
                    telefoneDDDContato = null,
                    telefoneRamalContato = null,
                    telefoneNumeroContato = c.telefoneNumeroContato,
                    telefoneTipoContato = null
                });
            }

            List<CoberturaAfetada> listaCoberturaAfetada = new();
            listaCoberturaAfetada.Add(new CoberturaAfetada() { codigoCoberturaAfetada = null });

            this.listaSinistro = new List<Sinistro>();
            foreach (var c in dadosInspecao.listaSinistros)
            {

                this.listaSinistro.Add(new Sinistro()
                {
                    numeroSinistro = c.numeroSinistro.ToString(),
                    causaGeradora = c.causaGeradora,
                    dataOcorrencia = c.dataOcorrencia,
                    valorSinistro = c.valorSinistro.ToString(),
                    listaCoberturaAfetada = listaCoberturaAfetada,
                    statusSinistro = null
                });
            }

            this.listaCamposVariaveis = new List<CamposVariaveis>();
            foreach (var c in dadosInspecao.listaCamposVariaveis)
            {
                this.listaCamposVariaveis.Add(new CamposVariaveis()
                {
                    descricaoCampo = c.descricaoCampo,
                    conteudoCampo = c.conteudoCampo
                });
            }
        }
    }
}