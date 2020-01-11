using System.Collections.Generic;
using HelpfulThings.Connect.Quandl.Models.Shared;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatabaseModels
{
    public class DatabaseSearchResults
    {
        [JsonProperty(PropertyName = "databases")]
        public List<DatabaseMetadata> Databases { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public PagingMetadata Metadata { get; set; }
    }
}
