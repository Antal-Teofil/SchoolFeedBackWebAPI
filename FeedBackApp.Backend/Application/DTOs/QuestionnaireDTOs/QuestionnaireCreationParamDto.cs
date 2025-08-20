namespace Application.DTOs.QuestionnaireDTOs
{
    public class QuestionnaireCreationParamDto
    {
        public string TeacherEmail { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public List<string> StudentSetIds { get; set; } = new();
    }
}
