using Newtonsoft.Json;

namespace HelpfulThings.Connect.Quandl.Models.Datatable
{
    public class CursorMetadata
    {
        [JsonProperty(PropertyName = "next_cursor_id")]
        public string NextCursorId { get; set; }
    }
}
