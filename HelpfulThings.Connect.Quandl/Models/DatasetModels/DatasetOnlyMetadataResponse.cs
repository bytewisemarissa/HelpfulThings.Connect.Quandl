using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatasetModels
{
    public class DatasetOnlyMetadataResponse
    {
        [JsonProperty(PropertyName = "dataset")]
        public DatasetOnlyMetadataModel DatasetMetadataOnly { get; set; }
    }
}
