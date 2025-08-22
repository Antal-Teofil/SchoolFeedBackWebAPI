using FeedBackApp.Core.Model.Enum;
using Newtonsoft.Json;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class QuestionTemplateDto
    {
        [JsonProperty("question")]
        public string Question { get; set; } = string.Empty;

        [JsonProperty("type")]
        public QuestionType? Type { get; set; }
    }
}