using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Dataset
{
    public class OnlyData
    {
        [JsonProperty(PropertyName = "limit")]
        public int? Limit { get; set; }

        [JsonProperty(PropertyName = "transform")]
        public string Transform { get; set; }

        [JsonProperty(PropertyName = "column_index")]
        public int? ColumnIndex { get; set; }

        [JsonProperty(PropertyName = "column_names")]
        public List<string> ColumnNames { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateTime? EndDate { get; set; }

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
