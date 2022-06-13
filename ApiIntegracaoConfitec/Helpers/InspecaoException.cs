using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ApiIntegracaoConfitec.Helpers
{
    [ExcludeFromCodeCoverage]
    public class InspecaoException : Exception
    {
        public List<string> listValidationResult { get; set; }

        public InspecaoException() : base()
        {

        }

        public InspecaoException(string message) : base(message)
        {

        }

        public InspecaoException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            this.listValidationResult = (List<String>)args[0];
        }
    }
}
