using System.Collections.Generic;

namespace ApiIntegracaoConfitec.Interfaces.Controller
{
    public interface IResultHttpResponse
    {
        bool Success { get; set; }
        string Message { get; set; }
        List<string> Errors { get; set; }
    }
}
