using System.Collections.Generic;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class PedidoInspecao
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
        public List<ListaCoberturaInspecao> listaCoberturaInspecao { get; set; }
        public List<ListaTelefoneContato> listaTelefoneContato { get; set; }
        public List<ListaSinistro> listaSinistro { get; set; }
        public List<ListaCamposVariaveis> listaCamposVariaveis { get; set; }
    }
}