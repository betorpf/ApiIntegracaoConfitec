
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class SolicitarInspecaoRequest : IRequest<CancelarInspecaoResponse>
    {
        [Required(ErrorMessage = "Número do PI não informado")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um PI maior que zero")]
        public int PI { get; set; }
    }
}
