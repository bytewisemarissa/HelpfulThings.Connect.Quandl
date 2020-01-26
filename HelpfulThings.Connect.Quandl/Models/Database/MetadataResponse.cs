using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Database
{
    public class MetadataResponse
    {
        [JsonProperty(PropertyName = "database")]
        public Metadata Database { get; set; }
    }
}
