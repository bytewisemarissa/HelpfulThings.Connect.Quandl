using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatatableModels
{
    public class DatatableResponse
    {
        [JsonProperty(PropertyName = "datatable")]
        public QuandlDatatableModel Datatable { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public CursorMetadata CursorMetadata { get; set; }
    }
}
