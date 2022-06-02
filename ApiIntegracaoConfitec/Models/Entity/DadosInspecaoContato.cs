using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Entity
{
    public class DadosInspecaoContato
    {

        [Required(ErrorMessage = "Nome do Contato 1 não informado.", AllowEmptyStrings = false)]
        public string nomeContato { get; set; }

        [Required(ErrorMessage = "Número do Telefone do Contato 1 não informado.", AllowEmptyStrings = false)]
        public string telefoneNumeroContato { get; set; }
    }
}
