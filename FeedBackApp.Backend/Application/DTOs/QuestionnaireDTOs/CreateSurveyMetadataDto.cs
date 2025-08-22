using Newtonsoft.Json;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class CreateSurveyMetadataDto
    {
        [JsonProperty("startDate")]
        public string StartDate { get; set; } = string.Empty;

        [JsonProperty("endDate")]
        public string EndDate { get; set; } = string.Empty;

        [JsonProperty("studentSets")]
        public List<StudentSetDto> StudentSets { get; set; } = new();

        [JsonProperty("questionnaireTemplate")]
        public List<QuestionTemplateDto> QuestionTemplates { get; set; } = new();

        [JsonProperty("teachers")]
        public List<MetaTeacherDto> Teachers { get; set; } = new();
        
        [JsonProperty("questionnaireCreationParams")]
        public List<QuestionnaireCreationParamDto> CreationParams { get; set; } = new();
    }
}
