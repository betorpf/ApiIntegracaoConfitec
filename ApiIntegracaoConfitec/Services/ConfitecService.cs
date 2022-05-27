using ApiIntegracaoConfitec.Interfaces.Service;
using ApiIntegracaoConfitec.Interfaces.Services;
using ApiIntegracaoConfitec.Models.Confitec;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        public async Task<ResponseToken> Autenticacao(RequestToken requestToken)
        {
            ResponseToken responseTokenTeste = new ResponseToken() { access_token = "asfasd5f4asd6f54as65df46a5sdf465asd4f"};
            return responseTokenTeste;


            ResponseToken responseToken = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"{this._confitecApiURL}");

                    var jsonContent = JsonConvert.SerializeObject(requestToken);
                    var requestString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    requestString.Headers.ContentType = new
                    MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PostAsync("/token", requestString);
                    if (response.IsSuccessStatusCode)
                    {
                        string confitecResponse = await response.Content.ReadAsStringAsync();

                        responseToken = JsonConvert.DeserializeObject<ResponseToken>(confitecResponse);
                    }
                }
            }
            catch (System.Exception ex)
            {
                //TODO: IMPLEMENTAR
                throw;
            }
            return responseToken;
        }

        //TODO: Implementar
        public async Task<ConfitecSolicitarInspecao> SolicitarInspecao(RequestSolicitacaoInspecao pedidoInspecao, string access_token = null)
        {
            ConfitecSolicitarInspecao responseSolicitacaoInspecao = null;
            string confitecResponse = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string jsonContent = JsonConvert.SerializeObject(pedidoInspecao);
                    //TODO: AGUARDAR CONFITEC
                    //confitecResponse = await this.GenericPost("/inspecao/pedido/async", jsonContent, access_token);
                    confitecResponse = "{\"numeroInspecao\": 1,\"dataProcessamento\": \"20/05/2022\",\"codigoResultado\": 1,\"mensagemRetorno\": \"\",\"protocoloAbertura\": \"Sucesso\",\"erros\": null}";
                    responseSolicitacaoInspecao = JsonConvert.DeserializeObject<ConfitecSolicitarInspecao>(confitecResponse);
                }
            }
            catch (System.Exception ex)
            {
                //TODO: IMPLEMENTAR
                throw;
            }
            return responseSolicitacaoInspecao;
        }

        //TODO: Implementar
        public async Task<ResponseCancelarInspecao> CancelarInspecao(RequestCancelamentoInspecao requestCancelamentoInspecao, string access_token = null)
        {
            ResponseCancelarInspecao responseCancelamentoInspecao = null;
            try
            {
                string jsonContent = JsonConvert.SerializeObject(requestCancelamentoInspecao);
                string confitecResponse = await this.GenericPost("/inspecao/cancelamento/async", jsonContent, access_token);
                responseCancelamentoInspecao = JsonConvert.DeserializeObject<ResponseCancelarInspecao>(confitecResponse);
            }
            catch (System.Exception)
            {

                throw;
            }
            return responseCancelamentoInspecao;
        }

        public async Task<string> GenericPost(string method, string jsonContent, string access_token = null)
        {
            string responseString = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"{this._confitecApiURL}");

                    
                    var requestString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    requestString.Headers.ContentType = new
                    MediaTypeHeaderValue("application/json");
                    if(!String.IsNullOrEmpty(access_token))
                        requestString.Headers.Add("Authorization", access_token.ToString());

                    HttpResponseMessage response = await client.PostAsync(method, requestString);
                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return responseString;
        }
    }
}
