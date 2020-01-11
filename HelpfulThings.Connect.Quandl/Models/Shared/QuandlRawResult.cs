using HelpfulThings.Connect.Quandl.TypedEnums;

namespace HelpfulThings.Connect.Quandl.Models.Shared
{
    public class QuandlRawResult
    {
        public ReturnFormat RequestedType { get; set; }
        public string RawResult { get; set; }
    }
}
