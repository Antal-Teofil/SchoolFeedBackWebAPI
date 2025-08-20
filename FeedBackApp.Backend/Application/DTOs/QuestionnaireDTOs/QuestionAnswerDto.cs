using FeedBackApp.Core.Model.Enum;

namespace Application.DTOs.QuestionnaireDTOs
{
    public class QuestionAnswerDto
    {
        public string Question { get; set; } = string.Empty;
        public QuestionType Type { get; set; } = QuestionType.Type1;
        public string? Answer { get; set; }
    }
}