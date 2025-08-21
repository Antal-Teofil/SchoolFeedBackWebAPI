
using FeedBackApp.Core.Model.Enum;

namespace FeedBackApp.Core.Model
{
    public class QuestionTemplate
    {
        public string Question { get; set; } = string.Empty;

        public QuestionType Type { get; set; }
    }
}
