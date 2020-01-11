using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Shared
{
    public class ErrorResponse
    {
        [JsonProperty(PropertyName = "quandl_error")]
        public QuandlError Error { get; set; }
    }
}
