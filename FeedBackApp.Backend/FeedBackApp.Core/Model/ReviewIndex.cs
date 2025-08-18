
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class ReviewIndex
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = "reviewIndex";

        [JsonPropertyName("surveyid")]
        public string SurveyId { get; set; } = string.Empty; // is this neccessary?

        [JsonPropertyName("entries")]
        public List<ReviewEntry> Entries { get; set; } = new();
    }
}
