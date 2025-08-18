
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class QuestionAnswer
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        
        [JsonPropertyName("answer")]
        public string? Answer { get; set; } = null;
    }
}
