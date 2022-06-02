using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Entity
{
    public class DadosInspecaoCamposVariaveis
    {
        [Required(ErrorMessage = "Descrição Campo não informada.", AllowEmptyStrings = false)]
        public string descricaoCampo { get; set; }

        [Required(ErrorMessage = "Conteúdo Campo não informado.", AllowEmptyStrings = false)]
        public string conteudoCampo { get; set; }
    }
}
