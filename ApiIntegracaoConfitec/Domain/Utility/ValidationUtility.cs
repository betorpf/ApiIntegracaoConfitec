using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegracaoConfitec.Domain.Utility
{
    public static class ValidationUtility
    {
        public static string ValidateObject(object obj)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, resultadoValidacao, true);
            return String.Join(",", resultadoValidacao);
        }
    }
}
