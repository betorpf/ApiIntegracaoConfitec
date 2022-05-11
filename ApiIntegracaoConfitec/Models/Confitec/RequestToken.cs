using ApiIntegracaoConfitec.Interfaces.Services;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class RequestToken : IRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get { return "password"; } }

    }
}
