using Newtonsoft.Json;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class QuestionAnswerDto
    {
        [JsonProperty("answer")]
        public string? Answer { get; set; } = null;
    }
}