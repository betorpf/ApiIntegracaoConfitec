using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class ResultadoInspecaoCobertura
    {
        [Required(ErrorMessage = "")]
        public string codigoCobertura { get; set; }
        public string descricaoParecer { get; set; }
        public string tipoParecer { get; set; }
    }
}
