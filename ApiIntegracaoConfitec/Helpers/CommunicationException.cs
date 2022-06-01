using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApiIntegracaoConfitec.Helpers
{
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
