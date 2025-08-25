using Newtonsoft.Json;

namespace Application.DTOs.Questionnaire
{
    public class UpdateQuestionnaireDTO
    {
        [JsonProperty("questionnaireResult")]
        public List<QuestionResultDTO> QuestionnaireResult { get; set; } = new List<QuestionResultDTO>();

    }
}
