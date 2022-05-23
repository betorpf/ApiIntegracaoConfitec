namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaInspecaoResponse
    {
        public bool success { get; set; }
        public GravarRespostaInspecaoResponse(bool success)
        {
            this.success = success;
        }
    }
}
