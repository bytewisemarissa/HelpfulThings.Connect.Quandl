using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Dataset
{
    public class OnlyMetadataResponse
    {
        [JsonProperty(PropertyName = "dataset")]
        public OnlyMetadata MetadataOnly { get; set; }
    }
}
