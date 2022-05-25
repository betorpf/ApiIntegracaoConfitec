using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Entity
{
    public class DadosAutenticacao
    {
        [Required(ErrorMessage = "Usuário Sompo não informado.")]
        public string SompoUsername { get; set; }
        [Required(ErrorMessage = "Password Sompo não informado.")]
        public string SompoPassword { get; set; }
    }
}
