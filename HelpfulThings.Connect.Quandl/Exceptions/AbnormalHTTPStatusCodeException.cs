using System;

namespace HelpfulThings.Connect.Quandl.Exceptions
{
    public class AbnormalHttpStatusCodeException : Exception
    {
        public AbnormalHttpStatusCodeException(string message) : base(message) { }
        public AbnormalHttpStatusCodeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
