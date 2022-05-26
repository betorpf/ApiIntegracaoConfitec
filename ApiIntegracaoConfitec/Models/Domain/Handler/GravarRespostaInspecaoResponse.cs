namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaInspecaoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public GravarRespostaInspecaoResponse(bool Success, string message)
        {
            this.Success = Success;
            this.Message = message;
        }
    }
}
