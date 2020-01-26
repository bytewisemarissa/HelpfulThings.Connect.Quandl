using System.Collections.Generic;
using HelpfulThings.Connect.Quandl.Models.Shared;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Database
{
    public class ListingResult
    {
        [JsonProperty(PropertyName = "databases")]
        public List<Metadata> Databases { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public PagingMetadata PagingMetadata { get; set; } 
    }
}
