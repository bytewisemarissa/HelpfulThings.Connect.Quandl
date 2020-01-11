using System;

namespace HelpfulThings.Connect.Quandl.Exceptions
{
    public class InvalidDatatableShortCodeCombinationException : Exception
    {
        public InvalidDatatableShortCodeCombinationException(string message) : base(message) { }
        public InvalidDatatableShortCodeCombinationException(string message, Exception ex) : base(message, ex) { } 
    }
}
