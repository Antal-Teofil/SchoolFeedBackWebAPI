
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class QuestionnaireCreationParam
    {
        [JsonPropertyName("teacherEmail")]
        public string TeacherEmail { get; set; } = string.Empty;

        [JsonPropertyName("subjectName")]
        public string SubjectName { get; set; } = string.Empty;

        [JsonPropertyName("studentSetId")]
        public List<string> StudentSetIds { get; set; } = new();
    }
}
