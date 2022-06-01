using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiIntegracaoConfitec.Domain.Utility
{
    public static class ValidationUtility
    {
        public static List<string> ListValidateObject(object obj)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, resultadoValidacao, true);
            return resultadoValidacao.Select(x => x.ToString()).ToList();
        }

    }
}
