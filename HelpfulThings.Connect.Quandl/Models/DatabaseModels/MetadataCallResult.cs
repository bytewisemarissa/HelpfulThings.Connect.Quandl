using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatabaseModels
{
    public class MetadataCallResult
    {
        [JsonProperty(PropertyName = "database")]
        public DatabaseMetadata Database { get; set; }
    }
}
