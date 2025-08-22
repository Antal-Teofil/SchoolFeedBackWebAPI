using Newtonsoft.Json;

namespace Application.DTOs.Questionnaire
{
    public class QuestionAnswerDto
    {
        [JsonProperty("answer")]
        public string Answer { get; set; } = string.Empty;
    }
}