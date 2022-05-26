using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Entity
{
    public class DadosAutenticacao
    {
        [Required(ErrorMessage = "Usuário Sompo não informado.", AllowEmptyStrings = false)]
        public string SompoUsername { get; set; }

        [Required(ErrorMessage = "Password Sompo não informado.", AllowEmptyStrings = false)]
        public string SompoPassword { get; set; }
    }
}
