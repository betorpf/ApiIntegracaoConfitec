using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Interfaces.Service;
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
            ResponseToken responseTokenTeste = new ResponseToken() { access_token = "asfasd5f4asd6f54as65df46a5sdf465asd4f" };
            return responseTokenTeste;

            ResponseToken responseToken = null;
            string confitecResponse = "";
            try
            {

                using (var client = new HttpClient())
                {
                    var jsonContent = JsonConvert.SerializeObject(requestToken);
                    //TODO: AGUARDAR CONFITEC
                    confitecResponse = await this.GenericPost("/token", jsonContent);
                    responseToken = JsonConvert.DeserializeObject<ResponseToken>(confitecResponse);
                }
            }
            catch (System.Exception ex)
            {
                throw new CommunicationException("Ocorreu um erro ao autenticar na API da Confitec. Método: ConfitecService.Autenticacao - Erro: " + ex.Message);
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
                    //confitecResponse = "{\"numeroInspecao\": 1,\"dataProcessamento\": \"20/05/2022\",\"codigoResultado\": 1,\"mensagemRetorno\": \"\",\"protocoloAbertura\": \"Sucesso\",\"erros\": [ { \"CodigoErro\":\"321\", \"DescricaoErro\":\"ERRO GENERICO\" } ]}";
                    confitecResponse = "{\"numeroInspecao\": 1,\"dataProcessamento\": \"20/05/2022\",\"codigoResultado\": 1,\"mensagemRetorno\": \"\",\"protocoloAbertura\": \"Sucesso\",\"erros\": null }";
                    responseSolicitacaoInspecao = JsonConvert.DeserializeObject<ConfitecSolicitarInspecao>(confitecResponse);
                }
            }
            catch (System.Exception ex)
            {
                throw new CommunicationException("Ocorreu um erro ao solicitar inspeção na API da Confitec. Método: ConfitecService.SolicitarInspecao - Erro: " + ex.Message);
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
            catch (System.Exception ex)
            {
                throw new CommunicationException("Ocorreu um erro ao cancelar inspeção na API da Confitec. Método: ConfitecService.CancelarInspecao - Erro: " + ex.Message);
            }
            return responseCancelamentoInspecao;
        }

        public async Task<string> GenericPost(string method, string jsonContent, string access_token = null)
        {
            string responseString = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{this._confitecApiURL}");


                var requestString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                requestString.Headers.ContentType = new
                MediaTypeHeaderValue("application/json");
                if (!String.IsNullOrEmpty(access_token))
                    requestString.Headers.Add("Authorization", access_token.ToString());

                HttpResponseMessage response = await client.PostAsync(method, requestString);
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                }
            }

            return responseString;
        }
    }
}
