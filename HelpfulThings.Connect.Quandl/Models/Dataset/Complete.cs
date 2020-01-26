using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Dataset
{
    public class Complete
    {
        [JsonProperty(PropertyName = "id")]
        public int DatasetId { get; set; }

        [JsonProperty(PropertyName = "dataset_code")]
        public string DatasetCode { get; set; }

        [JsonProperty(PropertyName = "database_code")]
        public string DatabaseCode { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "refreshed_at")]
        public DateTime RefreshedAt { get; set; }

        [JsonProperty(PropertyName = "newest_available_date")]
        public DateTime NewestDateAvailable { get; set; }

        [JsonProperty(PropertyName = "oldest_available_date")]
        public DateTime OldestDateAvailable { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "premium")]
        public bool Premium { get; set; }

        [JsonProperty(PropertyName = "database_id")]
        public int DatabaseId { get; set; }

        [JsonProperty(PropertyName = "limit")]
        public int? Limit { get; set; }

        [JsonProperty(PropertyName = "transform")]
        public string Transform { get; set; }

        [JsonProperty(PropertyName = "column_index")]
        public int? ColumnIndex { get; set; }

        [JsonProperty(PropertyName = "column_names")]
        public List<string> ColumnNames { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "frequency")]
        public string Frequency { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<List<string>> TableData { get; set; }

        [JsonProperty(PropertyName = "collapse")]
        public string Collapse { get; set; }

        [JsonProperty(PropertyName = "order")]
        public string Order { get; set; }
    }
}
