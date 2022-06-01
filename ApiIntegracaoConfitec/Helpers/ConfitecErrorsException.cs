using ApiIntegracaoConfitec.Models.Confitec;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApiIntegracaoConfitec.Helpers
{
    public class ConfitecErrorsException : Exception
    {
        public List<ErroConfitec> listErrors { get; set; }

        public ConfitecErrorsException() : base()
        {

        }

        public ConfitecErrorsException(string message) : base(message)
        {

        }

        public ConfitecErrorsException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            this.listErrors = ((List<ErroConfitec>)args[0]);
        }
    }
}
