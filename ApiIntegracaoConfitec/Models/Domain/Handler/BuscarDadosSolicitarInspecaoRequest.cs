using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscarDadosSolicitarInspecaoRequest
    {
        [Required(ErrorMessage = "Número do PI não informado")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um Número PI maior que zero")]
        public int Num_PI { get; set; }

        [Required(ErrorMessage = "Número do Local não informado")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um Número Local maior que zero")]
        public int Num_Local { get; set; }

        [Required(ErrorMessage = "Tipo de Emissao não informado")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um Tipo de Emissao maior que zero")]
        public int Tip_Emissao { get; set; }

        public BuscarDadosSolicitarInspecaoRequest(int Num_PI, int Num_Local, int Tip_Emissao)
        {
            this.Num_PI = Num_PI;
            this.Num_Local = Num_Local;
            this.Tip_Emissao = Tip_Emissao;

            var ListaValidacao = ValidationUtility.ListValidateObject(this);
            if (ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Validação de Dados informados", ListaValidacao);
            }
        }
    }
}
