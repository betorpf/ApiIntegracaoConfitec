using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ApiIntegracaoConfitec.Helpers
{
    public class BRQValidationException : Exception
    {
        public List<string> listValidationResult { get; set; }

        public BRQValidationException() : base()
        {

        }

        public BRQValidationException(string message) : base(message)
        {

        }

        public BRQValidationException(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            this.listValidationResult = (List<String>)args[0];
        }
    }
}
