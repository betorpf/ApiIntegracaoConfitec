namespace ApiIntegracaoConfitec.Models.Domain.Handler
{
    public class BuscaDadosSolicitarInspecaoResponse
    {
        public int PI { get; set; }
        public string Codigo { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
