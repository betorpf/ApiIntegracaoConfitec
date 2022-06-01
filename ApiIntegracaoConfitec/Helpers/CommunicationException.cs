using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ApiIntegracaoConfitec.Helpers
{
    [ExcludeFromCodeCoverage]
    public class CommunicationException : Exception
    {
        public List<string> listValidationResult { get; set; }

        public CommunicationException() : base()
        {

        }

        public CommunicationException(string message) : base(message)
        {

        }

        public CommunicationException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            this.listValidationResult = (List<String>)args[0];
        }
    }
}
