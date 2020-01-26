using System.Collections.Generic;
using HelpfulThings.Connect.Quandl.Models.Shared;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Dataset
{
    public class SearchResult
    {
        [JsonProperty(PropertyName = "datasets")]
        public List<OnlyMetadata> Datasets { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public PagingMetadata Metadata { get; set; } 
    }
}
