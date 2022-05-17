
using MediatR;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class CancelarInspecaoRequest : IRequest<CancelarInspecaoResponse>
    {
        public int PI { get; set; }
    }
}
