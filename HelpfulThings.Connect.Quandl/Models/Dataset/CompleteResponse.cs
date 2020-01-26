using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Dataset
{
    public class CompleteResponse
    {
        [JsonProperty(PropertyName = "dataset")]
        public Complete Complete { get; set; }
    }
}
