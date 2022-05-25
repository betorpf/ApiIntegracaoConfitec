using ApiIntegracaoConfitec.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class ResponseToken : IResponse
    {
        [Required(ErrorMessage="Access Token inválido. Verificar método de autenticação com a Confitec.")]
        public string access_token { get; set; }
    }
}
