
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class Metadata
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("teachers")]
        public IList<MetaTeacher> Teachers { get; set; } // = new List<MetaTeacher>();

        [JsonPropertyName("startdate")]
        public string StartDate { get; set; } = string.Empty;

        [JsonPropertyName("enddate")]
        public string EndDate { get; set; } = string.Empty;

        [JsonPropertyName("questionnaire")]
        public IDictionary<string, string> Questionnaire { get; set; } // = new Dictionary<string, string>();
    }
}
