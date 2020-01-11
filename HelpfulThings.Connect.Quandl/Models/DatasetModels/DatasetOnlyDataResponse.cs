using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatasetModels
{
    public class DatasetOnlyDataResponse
    {
        [JsonProperty(PropertyName = "dataset_data")]
        public DatasetDataOnlyModel DatasetDataOnly { get; set; }
    }
}
