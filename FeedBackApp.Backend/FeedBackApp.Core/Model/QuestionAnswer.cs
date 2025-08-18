
using FeedBackApp.Core.Model.Enum;
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class QuestionAnswer
    {
        [JsonPropertyName("type")]
        public QuestionType Type { get; set; } = QuestionType.Type1;
        
        [JsonPropertyName("answer")]
        public string? Answer { get; set; } = null;
    }
}
