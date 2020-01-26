using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Datatable
{
    public class Response
    {
        [JsonProperty(PropertyName = "datatable")]
        public Datatable Datatable { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public CursorMetadata CursorMetadata { get; set; }
    }
}
