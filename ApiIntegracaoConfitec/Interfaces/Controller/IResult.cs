using System.Net;
using System.Text.Json.Serialization;

namespace ApiIntegracaoConfitec.Interfaces.Controller
{
    public interface IResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        [JsonIgnore]
        HttpStatusCode StatusCode { get; set; }
    }
}
