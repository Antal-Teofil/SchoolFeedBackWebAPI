using Newtonsoft.Json;

namespace Application.DTOs.Questionnaire
{
    public class UpdateQuestionnaireDTO
    {
        [JsonProperty("questionnareResult")]
        public List<QuestionnaireResult> QuestionnaireResult { get; set; } = new List<QuestionnaireResult>();

    }
}
