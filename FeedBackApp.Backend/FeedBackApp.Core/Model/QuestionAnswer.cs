
using FeedBackApp.Core.Model.Enum;

namespace FeedBackApp.Core.Model
{
    public class QuestionAnswer
    {
        public string Question { get; set; } = string.Empty;

        public QuestionType Type { get; set; }

        public string? Answer { get; set; } = null;
    }
}
