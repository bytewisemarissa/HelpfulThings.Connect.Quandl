using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatatableModels
{
    public class QuandlColumn
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
