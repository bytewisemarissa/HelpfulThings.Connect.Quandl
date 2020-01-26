using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Database
{
    public class Metadata
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "database_code")]
        public string DatabaseCode { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "datasets_count")]
        public int DatasetsCount { get; set; }

        [JsonProperty(PropertyName = "downloads")]
        public long Downloads { get; set; }

        [JsonProperty(PropertyName = "premium")]
        public bool Premium { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "favorite")]
        public bool Favorite { get; set; }
    }
}
