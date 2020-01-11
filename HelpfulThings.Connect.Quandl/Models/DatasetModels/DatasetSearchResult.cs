using System.Collections.Generic;
using HelpfulThings.Connect.Quandl.Models.Shared;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatasetModels
{
    public class DatasetSearchResult
    {
        [JsonProperty(PropertyName = "datasets")]
        public List<DatasetOnlyMetadataModel> Datasets { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public PagingMetadata Metadata { get; set; } 
    }
}
