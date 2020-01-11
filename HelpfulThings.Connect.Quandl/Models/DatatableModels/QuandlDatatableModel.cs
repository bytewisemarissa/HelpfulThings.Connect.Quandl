using System.Collections.Generic;
using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatatableModels
{
    public class QuandlDatatableModel
    {
        [JsonProperty(PropertyName = "data")]
        public List<List<string>> Data { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public List<QuandlColumn> Columns { get; set; } 
    }
}
