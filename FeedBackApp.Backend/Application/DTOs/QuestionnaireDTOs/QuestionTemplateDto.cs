using FeedBackApp.Core.Model.Enum;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class QuestionTemplateDto
    {
        public string Question { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
    }
}