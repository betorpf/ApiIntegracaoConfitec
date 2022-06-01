using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class ResultadoInspecao
    {
        [Required(ErrorMessage = "")]
        public long numeroOrcamento { get; set; }

        [Required(ErrorMessage = "")]
        public long numeroVersaoOrcamento { get; set; }

        [Required(ErrorMessage = "")]
        public int numeroItemVersaoOrcamento { get; set; }

        [Required(ErrorMessage = "")]
        public long numeroCnpjCpf { get; set; }

        [Required(ErrorMessage = "")]
        public string numeroSolicitacaoInspecao { get; set; }

        [Required(ErrorMessage = "")]
        public string flagLocalRiscoEnderecoInspecao { get; set; }

        [Required(ErrorMessage = "")]
        public string nomeContato { get; set; }

        [Required(ErrorMessage = "")]
        public string numeroTelefoneContato { get; set; }

        [Required(ErrorMessage = "")]
        public int codigoTipoLogradouro { get; set; }
        
        public int codigoLogradouro { get; set; }

        [Required(ErrorMessage = "")]
        public string nomeLogradouro { get; set; }
        
        public int numeroLogradouro { get; set; }

        [Required(ErrorMessage = "")]
        public long numeroCep { get; set; }
        
        public string nomeComplemento { get; set; }

        [Required(ErrorMessage = "")]
        public string nomeBairro { get; set; }

        [Required(ErrorMessage = "")]
        public string nomeCidade { get; set; }

        [Required(ErrorMessage = "")]
        public int codigoUnidadeFederacao { get; set; }

        [Required(ErrorMessage = "")]
        public int codigoPais { get; set; }
        
        public string textoPontoReferencia { get; set; }

        [Required(ErrorMessage = "")]
        public decimal numeroLatitude { get; set; }

        [Required(ErrorMessage = "")]
        public decimal numeroLongitude { get; set; }

        [Required(ErrorMessage = "")]
        public string codigoStatusSolicitacaoInspecao { get; set; }

        [Required(ErrorMessage = "")]
        public string codigoStatusParecerSolicitacaoInspecao { get; set; }

        [Required(ErrorMessage = "")]
        public string flagSolicitacaoInspecaoAutomatico { get; set; }

        [Required(ErrorMessage = "")]
        public DateTime dataSolicitacaoInspecao { get; set; }

        [Required(ErrorMessage = "")]
        public string flagInspetorConfiavel { get; set; }
        
        public string codigoAtividade { get; set; }

        [Required(ErrorMessage = "")]
        public string flagCodigoAtividadeAlterada { get; set; }

        [Required(ErrorMessage = "")]
        public string flagEnderecoAlterado { get; set; }

        [Required(ErrorMessage = "")]
        public string flagAtivo { get; set; }

        [Required(ErrorMessage = "")]
        public List<ResultadoInspecaoCobertura> listaCoberturas { get; set; }
        
        public int motivoInspecao { get; set; }
        
        public DateTime dataAgendamento { get; set; }
        
        public int numeroSinistro { get; set; }
    }
}
