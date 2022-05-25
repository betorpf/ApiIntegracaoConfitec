using ApiIntegracaoConfitec.Models.Sompo.Controller;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Confitec.Controller
{
    public class RetornarDadosLaudoRequest : IRequest
    {
        [Required(ErrorMessage = "Número do PI não informado")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um PI maior que zero")]
        public int PI { get; set; }
    }
}
