
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class Questionnaire
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("surveyid")]
        public string SurveyId { get; set; } = string.Empty; // is this neccessary?

        [JsonPropertyName("answers")]
        public Dictionary<string, QuestionAnswer> Answers { get; set; } = new();
    }
}
