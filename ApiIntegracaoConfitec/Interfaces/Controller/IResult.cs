using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace ApiIntegracaoConfitec.Interfaces.Controller
{
    public interface IResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        List<string> Errors { get; set; }

        [JsonIgnore]
        HttpStatusCode StatusCode { get; set; }       
    }
}
