
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Sompo.Controller
{
    public class SolicitarInspecaoRequest
    {
        public int Num_PI { get; set; }
        public int Num_Local { get; set; }
        public int Tip_Emissao { get; set; }
    }
}
