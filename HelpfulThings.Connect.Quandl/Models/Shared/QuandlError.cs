using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Shared
{
    public class QuandlError
    {
        [JsonProperty(PropertyName = "code")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
