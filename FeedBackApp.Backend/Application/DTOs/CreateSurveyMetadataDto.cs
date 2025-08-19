
namespace Application.DTOs
{
    public class CreateSurveyMetadataDto
    {
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public List<StudentSetDto> StudentSets { get; set; } = new();
        public List<QuestionTemplateDto> QuestionnaireTemplate { get; set; } = new();
        public List<MetaTeacherDto> Teachers { get; set; } = new();
        public List<QuestionnaireCreationParamDto> CreationParams { get; set; } = new();
    }
}
