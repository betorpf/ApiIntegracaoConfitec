using ApiIntegracaoConfitec.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Services
{
    public class ConfitecService : IConfitecService
    {
        private readonly IConfiguration _config;
        private readonly string _confitecApiURL;

        public ConfitecService(IConfiguration config)
        {
            this._config = config;
            this._confitecApiURL = this._config.GetValue<string>("ConfitecApiURL").ToString();
        }

        //TODO: Implementar
        public async Task<string> Get()
        {
            try
            {
                return "";
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
