namespace ApiIntegracaoConfitec.Models.Entity
{
    public class DadosInspecao
    {
        public decimal codigoRamo { get; set; }
        public decimal codigoModalidade { get; set; }
        public string nomeContato { get; set; }
        public string telefoneNumeroContato { get; set; }
        public long codigoCorretorPrincipal { get; set; }
        public string numeroProposta { get; set; }
        public string numeroObjetoSegurado { get; set; }
        public decimal numeroApolice { get; set; }
        public decimal numeroEndosso { get; set; }
        public string codigoUf { get; set; }
        public string nomeMunicipio { get; set; }
        public string numeroCep { get; set; }
        public string nomeBairro { get; set; }
        public string nomeLogradouro { get; set; }
        public string numeroLogradouro { get; set; }
        public string nomeComplemento { get; set; }
        public string codigoTipoLogradouro { get; set; }
        public string codigoTipoPessoa { get; set; }
        public string nomeSegurado { get; set; }
        public string numeroCpfCnpjSegurado { get; set; }
        public decimal dataPedidoInspecao { get; set; }
        public string observacoes { get; set; }
        public string descricaoObjetoSegurado { get; set; }
        public decimal codigoCobertura { get; set; }
        public decimal valorLmi { get; set; }
        public string nomeContato1 { get; set; }
        public string telefoneNumeroContato1 { get; set; }
        public string nomeContato2 { get; set; }
        public string telefoneNumeroContato2 { get; set; }
        public decimal numeroSinistro { get; set; }
        public string causaGeradora { get; set; }
        public string dataOcorrencia { get; set; }
        public decimal valorSinistro { get; set; }
        public string descricaoCampo { get; set; }
        public string conteudoCampo { get; set; }
    }
}
