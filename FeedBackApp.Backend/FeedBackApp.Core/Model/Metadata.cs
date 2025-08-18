
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class Metadata
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("teachers")]
        public List<MetaTeacher> Teachers { get; set; } = new();

        [JsonPropertyName("startdate")]
        public string StartDate { get; set; } = string.Empty;

        [JsonPropertyName("enddate")]
        public string EndDate { get; set; } = string.Empty;

        [JsonPropertyName("questionnaire")]
        public Dictionary<string, string> Questionnaire { get; set; } = new();
    }
}
