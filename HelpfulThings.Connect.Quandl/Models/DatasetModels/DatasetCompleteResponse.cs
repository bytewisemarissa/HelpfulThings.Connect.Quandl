using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatasetModels
{
    public class DatasetCompleteResponse
    {
        [JsonProperty(PropertyName = "dataset")]
        public DatasetCompleteModel CompleteDataset { get; set; }
    }
}
