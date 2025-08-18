
namespace FeedBackApp.Core.Model
{
    public class ReviewEntry
    {
        public string TeacherEmail { get; set; } = string.Empty;

        public string TeacherName { get; set; } = string.Empty; // is this neccessary?

        public string SubjectName { get; set; } = string.Empty;

        public string StudentEmail { get; set; } = string.Empty;

        public string QuestionnaireId { get; set; } = string.Empty;
    }
}
