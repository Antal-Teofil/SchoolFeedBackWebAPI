using Newtonsoft.Json;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class CreateSurveyMetadataDto
    {
        [JsonProperty("startDate")]
        public DateOnly StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateOnly EndDate { get; set; }

        [JsonProperty("studentSets")]
        public List<StudentSetDto> StudentSets { get; set; } = new();

        [JsonProperty("questionnaireTemplate")]
        public List<QuestionTemplateDto> QuestionnaireTemplate { get; set; } = new();

        [JsonProperty("teachers")]
        public List<MetaTeacherDto> Teachers { get; set; } = new();
        
        [JsonProperty("questionnaireCreationParams")]
        public List<QuestionnaireCreationParamDto> CreationParams { get; set; } = new();
    }
}
