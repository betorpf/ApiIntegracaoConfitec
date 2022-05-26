using ApiIntegracaoConfitec.Interfaces.Services;
using System.Collections.Generic;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class ResponseCancelarInspecao : IResponse
    {
        public string numeroInspecao { get; set; }
        public string dataProcessamento { get; set; }
        public string codigoResultado { get; set; }
        public string mensagemRetorno { get; set; }
        public string protocoloAbertura { get; set; }
        public List<ErroConfitec> erros
        {
            get; set;
        }
    }
}
