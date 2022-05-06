using ApiIntegracaoConfitec.Interfaces.Service;
using ApiIntegracaoConfitec.Models.Confitec;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
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
        public async Task<string> Autenticacao(string metodo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"this._confitecApiURL{metodo}");
                    HttpResponseMessage response = await client.GetAsync($"product/");
                    if (response.IsSuccessStatusCode)
                    {
                        string productResponse = await response.Content.ReadAsStringAsync();

                       //product = JsonConvert.DeserializeObject<Product>(productResponse);

                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return null;
        }

        //TODO: Implementar
        public async Task<string> SolicitarInspecao(PedidoInspecao pedidoInspecao)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"{this._confitecApiURL}");
                    //HttpResponseMessage response = await client.PostAsync("/inspecao/pedido/async", pedidoInspecao);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    string productResponse = await response.Content.ReadAsStringAsync();

                    //    //product = JsonConvert.DeserializeObject<Product>(productResponse);

                    //}
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return null;
        }


        //TODO: Implementar
        public async Task<string> SolicitarLaudo(string metodo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"this._confitecApiURL{metodo}");
                    HttpResponseMessage response = await client.GetAsync($"product/");
                    if (response.IsSuccessStatusCode)
                    {
                        string productResponse = await response.Content.ReadAsStringAsync();

                        //product = JsonConvert.DeserializeObject<Product>(productResponse);

                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return null;
        }

        //TODO: Implementar
        public async Task<string> SolicitarCancelamento(string metodo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"this._confitecApiURL{metodo}");
                    HttpResponseMessage response = await client.GetAsync($"product/");
                    if (response.IsSuccessStatusCode)
                    {
                        string productResponse = await response.Content.ReadAsStringAsync();

                        //product = JsonConvert.DeserializeObject<Product>(productResponse);
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return null;
        }
    }
}
