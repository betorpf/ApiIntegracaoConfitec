using ApiIntegracaoConfitec.Domain.Utility;
using ApiIntegracaoConfitec.Models.Entity;

namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscarDadosSolicitarInspecaoResponse
    {
        public DadosInspecao dadosInspecao;
        public bool Success { get; set; }
        public string Message { get; set; }

        public BuscarDadosSolicitarInspecaoResponse(DadosInspecao dadosInspecao)
        {
            this.dadosInspecao = dadosInspecao;
            this.Message = ValidationUtility.ValidateObject(this.dadosInspecao);
            this.Success = true;

            if (!string.IsNullOrEmpty(this.Message))
                throw new System.Exception(message: this.Message);
        }
    }
}
