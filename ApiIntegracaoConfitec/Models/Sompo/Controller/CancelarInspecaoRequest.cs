using MediatR;
using System;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class CancelarInspecaoRequest : IRequest<CancelarInspecaoHttpResponse>
    {
        public Int64 Num_PI { get; set; }
    }
}
