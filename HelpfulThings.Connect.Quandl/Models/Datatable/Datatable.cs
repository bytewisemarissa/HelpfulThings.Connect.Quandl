using System.Collections.Generic;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Datatable
{
    public class Datatable
    {
        [JsonProperty(PropertyName = "data")]
        public List<List<string>> Data { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public List<ColumnMetadata> Columns { get; set; } 
    }
}
