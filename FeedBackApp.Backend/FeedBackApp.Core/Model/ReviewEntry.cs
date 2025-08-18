
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class ReviewEntry
    {
        [JsonPropertyName("teacheremail")]
        public string TeacherEmail { get; set; } = string.Empty;

        [JsonPropertyName("teachername")]
        public string TeacherName { get; set; } = string.Empty; // is this neccessary?

        [JsonPropertyName("subjectname")]
        public string SubjectName { get; set; } = string.Empty;

        [JsonPropertyName("studentemail")]
        public string StudentEmail { get; set; } = string.Empty;

        [JsonPropertyName("questionnaireid")]
        public string QuestionnaireId { get; set; } = string.Empty;
    }
}
