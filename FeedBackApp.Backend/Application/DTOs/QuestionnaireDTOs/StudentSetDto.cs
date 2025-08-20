namespace Application.DTOs.QuestionnaireDTOs
{
    public class StudentSetDto
    {
        public string SetId { get; set; } = string.Empty;
        public List<string> StudentEmails { get; set; } = new();
    }
}
