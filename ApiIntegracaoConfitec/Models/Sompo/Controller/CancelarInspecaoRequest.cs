
using MediatR;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class CancelarInspecaoRequest : IRequest<CancelarInspecaoHttpResponse>
    {
        public int PI { get; set; }
    }
}
