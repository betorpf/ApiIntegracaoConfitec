using System.Collections.Generic;

namespace ApiIntegracaoConfitec.Models.Confitec
{
    public class Sinistro
    {
        public string numeroSinistro { get; set; }
        public string statusSinistro { get; set; }
        public string causaGeradora { get; set; }
        public string dataOcorrencia { get; set; }
        public string valorSinistro { get; set; }
        public List<CoberturaAfetada> listaCoberturaAfetada { get; set; }
    }
}
