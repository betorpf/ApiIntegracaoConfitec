using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Entity
{
    public class DadosInspecao
    {
        [Required(ErrorMessage = "Código do Ramo não informado.", AllowEmptyStrings = false)]
        public decimal codigoRamo { get; set; }

        [Required(ErrorMessage = "Código da Modalidade não informado.", AllowEmptyStrings = false)]
        public decimal codigoModalidade { get; set; }

        [Required(ErrorMessage = "Nome do Contato não informado.", AllowEmptyStrings = false)]
        public string nomeContato { get; set; }

        [Required(ErrorMessage = "Telefone Número do Contato não informado.", AllowEmptyStrings = false)]
        public string telefoneNumeroContato { get; set; }

        [Required(ErrorMessage = "Código do Corretor Principal não informado.", AllowEmptyStrings = false)]
        public long codigoCorretorPrincipal { get; set; }

        [Required(ErrorMessage = "Número da Proposta não informado.", AllowEmptyStrings = false)]
        public string numeroProposta { get; set; }

        [Required(ErrorMessage = "Número do Objeto Segurado não informado.", AllowEmptyStrings = false)]
        public string numeroObjetoSegurado { get; set; }

        [Required(ErrorMessage = "Número da Apólice não informado.", AllowEmptyStrings = false)]
        public decimal numeroApolice { get; set; }

        [Required(ErrorMessage = "Número do Endosso não informado.", AllowEmptyStrings = false)]
        public decimal numeroEndosso { get; set; }

        [Required(ErrorMessage = "Código da UF não informado.", AllowEmptyStrings = false)]
        public string codigoUf { get; set; }

        [Required(ErrorMessage = "Município não informado.", AllowEmptyStrings = false)]
        public string nomeMunicipio { get; set; }

        [Required(ErrorMessage = "Número do CEP não informado.", AllowEmptyStrings = false)]
        public string numeroCep { get; set; }

        [Required(ErrorMessage = "Bairro não informado.", AllowEmptyStrings = false)]
        public string nomeBairro { get; set; }

        [Required(ErrorMessage = "Logradouro não informado.", AllowEmptyStrings = false)]
        public string nomeLogradouro { get; set; }

        [Required(ErrorMessage = "Número do Logradouro não informado.", AllowEmptyStrings = false)]
        public string numeroLogradouro { get; set; }

        [Required(ErrorMessage = "Complemento não informado.", AllowEmptyStrings = false)]
        public string nomeComplemento { get; set; }

        [Required(ErrorMessage = "Código do Tipo Logradouro não informado.", AllowEmptyStrings = false)]
        public string codigoTipoLogradouro { get; set; }

        [Required(ErrorMessage = "Código do Tipo da Pessoa não informado.", AllowEmptyStrings = false)]
        public string codigoTipoPessoa { get; set; }

        [Required(ErrorMessage = "Nome do Segurado não informado.", AllowEmptyStrings = false)]
        public string nomeSegurado { get; set; }

        [Required(ErrorMessage = "CPF/CNPJ não informado.", AllowEmptyStrings = false)]
        public string numeroCpfCnpjSegurado { get; set; }

        [Required(ErrorMessage = "Data do Pedido de Inspeção não informada.", AllowEmptyStrings = false)]
        public decimal dataPedidoInspecao { get; set; }

        [Required(ErrorMessage = "Observações não informado.", AllowEmptyStrings = false)]
        public string observacoes { get; set; }

        [Required(ErrorMessage = "Descrição do Objeto Segurado não informado.", AllowEmptyStrings = false)]
        public string descricaoObjetoSegurado { get; set; }
        public List<DadosInspecaoCobertura> listaCoberturas { get; set; }
        public List<DadosInspecaoContato> listaContatos { get; set; }
        public List<DadosInspecaoSinistro> listaSinistros { get; set; }
        public List<DadosInspecaoCamposVariaveis> listaCamposVariaveis { get; set; }


        //[Required(ErrorMessage = "Código da Cobertura não informado.", AllowEmptyStrings = false)]
        //public decimal codigoCobertura { get; set; }

        //[Required(ErrorMessage = "Valor LMI não informado.", AllowEmptyStrings = false)]
        //public decimal valorLmi { get; set; }

        //[Required(ErrorMessage = "Nome do Contato 1 não informado.", AllowEmptyStrings = false)]
        //public string nomeContato1 { get; set; }

        //[Required(ErrorMessage = "Número do Telefone do Contato 1 não informado.", AllowEmptyStrings = false)]
        //public string telefoneNumeroContato1 { get; set; }

        //[Required(ErrorMessage = "Nome do Contato 2 não informado.", AllowEmptyStrings = false)]
        //public string nomeContato2 { get; set; }

        //[Required(ErrorMessage = "Número do Telefone do Contato 2 não informado.", AllowEmptyStrings = false)]
        //public string telefoneNumeroContato2 { get; set; }

        //[Required(ErrorMessage = "Número do Sinistro não informado.", AllowEmptyStrings = false)]
        //public decimal numeroSinistro { get; set; }

        //[Required(ErrorMessage = "Causa Geradora não informada.", AllowEmptyStrings = false)]
        //public string causaGeradora { get; set; }

        //[Required(ErrorMessage = "Data da Ocorrência não informada.", AllowEmptyStrings = false)]
        //public string dataOcorrencia { get; set; }

        //[Required(ErrorMessage = "Valor do Sinistro não informado.", AllowEmptyStrings = false)]
        //public decimal valorSinistro { get; set; }

        //[Required(ErrorMessage = "Descrição Campo não informada.", AllowEmptyStrings = false)]
        //public string descricaoCampo { get; set; }

        //[Required(ErrorMessage = "Conteúdo Campo não informado.", AllowEmptyStrings = false)]
        //public string conteudoCampo { get; set; }
    }
}
