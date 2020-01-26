using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Datatable
{
    public class ColumnMetadata
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
