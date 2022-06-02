using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Entity
{
    public class DadosInspecaoCobertura
    {
        [Required(ErrorMessage = "Código da Cobertura não informado.", AllowEmptyStrings = false)]
        public decimal codigoCobertura { get; set; }

        [Required(ErrorMessage = "Valor LMI não informado.", AllowEmptyStrings = false)]
        public decimal valorLmi { get; set; }
    }
}
