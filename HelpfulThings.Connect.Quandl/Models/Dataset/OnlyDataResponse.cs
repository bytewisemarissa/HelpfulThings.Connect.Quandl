using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Dataset
{
    public class OnlyDataResponse
    {
        [JsonProperty(PropertyName = "dataset_data")]
        public OnlyData OnlyData { get; set; }
    }
}
