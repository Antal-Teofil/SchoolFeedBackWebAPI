
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class QuestionTemplate
    {
        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
