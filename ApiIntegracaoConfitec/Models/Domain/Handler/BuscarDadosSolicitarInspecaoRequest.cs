using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscarDadosSolicitarInspecaoRequest
    {
        [Required(ErrorMessage = "Número do PI não informado")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um PI maior que zero")]
        public int Num_PI { get; set; }

        public BuscarDadosSolicitarInspecaoRequest(int Num_PI)
        {
            this.Num_PI = Num_PI;
            var ListaValidacao = ValidationUtility.ListValidateObject(this);
            if (ListaValidacao.Count > 0)
            {
                throw new BRQValidationException("Validação de Número do PI informado.", ListaValidacao);
            }
        }
    }
}
