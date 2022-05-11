using ApiIntegracaoConfitec.Interfaces.Services;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class ResponseToken : IResponse
    {
        public string access_token { get; set; }
    }
}
