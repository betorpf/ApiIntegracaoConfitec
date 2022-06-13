using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Models.Entity
{
    public class ResultadoBusca
    {
        [Required(ErrorMessage = "Código do Resultado não informado.")]
        public int codigoResultado { get; set; }
        [Required(ErrorMessage = "Descrição do Resultado não informado.", AllowEmptyStrings = false)]
        public string descricaoResultado { get; set; }
        [Required(ErrorMessage = "Indicador para Solicitar Inspeção não informado.", AllowEmptyStrings = false)]
        public bool solicitarInspecao { get; set; }

        public override string ToString()
        {
            if (!solicitarInspecao)
                return $"Não é possível solicitar a inspeção. Cód.: {this.codigoResultado} - Descrição: {this.descricaoResultado}";
            else
                return $"É possível solicitar a inspeção. Cód.: {this.codigoResultado} - Descrição: {this.descricaoResultado}";
        }

    }
}