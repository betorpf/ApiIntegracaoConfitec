using ApiIntegracaoConfitec.Models.Sompo.Controller;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Helpers
{
    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (System.Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                DefaultResponse defaultResponse = new DefaultResponse();

                switch (error)
                {
                    case BRQValidationException e:
                        //Custon Validation Exception
                        defaultResponse.Errors = e.listValidationResult;
                        defaultResponse.Message = e.Message;
                        response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        break;
                    case ConfitecErrorsException e:
                        //Custon Validation Exception
                        defaultResponse.Errors = e.listErrors.Select(o => o.ToString()).ToList();
                        defaultResponse.Message = e.Message;
                        response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        break;
                    case InspecaoException e:
                        //Custom Inspeção Exception
                        defaultResponse.Message = e.Message;
                        response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        defaultResponse.Message = error.Message;
                        break;
                }

                var result = JsonSerializer.Serialize(defaultResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
