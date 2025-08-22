
namespace FeedBackApp.Core.Model
{
    public class QuestionnaireCreationParam
    {
        public string TeacherEmail { get; set; } = string.Empty;

        public string SubjectName { get; set; } = string.Empty;

        public IList<string> StudentSetIds { get; set; } = new List<string>();
    }
}
