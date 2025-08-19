
namespace Application.DTOs
{
    public class QuestionnaireDto
    {
        public string SurveyId { get; set; } = string.Empty;
        public string TeacherEmail { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public List<QuestionAnswerDto> QuestionnaireResults { get; set; } = new();
    }
}
