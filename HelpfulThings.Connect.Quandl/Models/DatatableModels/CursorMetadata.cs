using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.DatatableModels
{
    public class CursorMetadata
    {
        [JsonProperty(PropertyName = "next_cursor_id")]
        public string NextCursorId { get; set; }
    }
}
