using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Entity
{
    public class DadosInspecaoSinistro
    {
        [Required(ErrorMessage = "Número do Sinistro não informado.", AllowEmptyStrings = false)]
        public decimal numeroSinistro { get; set; }

        [Required(ErrorMessage = "Causa Geradora não informada.", AllowEmptyStrings = false)]
        public string causaGeradora { get; set; }

        [Required(ErrorMessage = "Data da Ocorrência não informada.", AllowEmptyStrings = false)]
        public string dataOcorrencia { get; set; }

        [Required(ErrorMessage = "Valor do Sinistro não informado.", AllowEmptyStrings = false)]
        public decimal valorSinistro { get; set; }


    }
}
