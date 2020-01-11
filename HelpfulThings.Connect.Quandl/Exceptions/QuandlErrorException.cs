using System;

namespace HelpfulThings.Connect.Quandl.Exceptions
{
    public class QuandlErrorException : Exception
    {
        public string Code { get; set; }

        public QuandlErrorException(string code, string message) : base(message)
        {
            this.Code = code;
        }

        public QuandlErrorException(string code, string message, Exception innerException) : base(message, innerException)
        {
            this.Code = code;
        }
    }
}
