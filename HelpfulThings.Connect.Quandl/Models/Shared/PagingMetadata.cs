using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Shared
{
    public class PagingMetadata
    {
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }

        [JsonProperty(PropertyName = "per_page")]
        public int ItemsPerPage { get; set; }

        [JsonProperty(PropertyName = "current_page")]
        public int CurrentPageIndex { get; set; }

        [JsonProperty(PropertyName = "prev_page")]
        public int? PreviousPageIndex { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "total_count")]
        public int TotalItemCount { get; set; }

        [JsonProperty(PropertyName = "next_page")]
        public int? NextPageIndex { get; set; }

        [JsonProperty(PropertyName = "current_first_item")]
        public int CurrentFirstItem { get; set; }

        [JsonProperty(PropertyName = "current_last_item")]
        public int CurrentLastItem { get; set; }
    }
}
