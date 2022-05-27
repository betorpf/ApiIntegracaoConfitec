﻿using ApiIntegracaoConfitec.Models.Sompo.Controller;
using Microsoft.AspNetCore.Http;
using System;
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

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(defaultResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
