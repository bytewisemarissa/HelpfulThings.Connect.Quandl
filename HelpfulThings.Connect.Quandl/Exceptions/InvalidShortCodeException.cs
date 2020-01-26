using System;

namespace HelpfulThings.Connect.Quandl.Exceptions
{
    public class InvalidShortCodeException : Exception
    {
        public InvalidShortCodeException(string message) : base(message) { }
        public InvalidShortCodeException(string message, Exception ex) : base(message, ex) { } 
    }
}
