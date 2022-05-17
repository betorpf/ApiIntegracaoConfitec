
using MediatR;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class SolicitarInspecaoRequest : IRequest<CancelarInspecaoResponse>
    {
        public int PI { get; set; }
    }
}
