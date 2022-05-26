namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class GravarRespostaCancelamentoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public GravarRespostaCancelamentoResponse(bool Success, string message)
        {
            this.Success = Success;
            this.Message = message;
        }
    }
}
