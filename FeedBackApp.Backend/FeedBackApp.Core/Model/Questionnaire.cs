
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class Questionnaire
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("partitionKey")]
        public string PartitionKey { get; set; } = string.Empty; // TeacherEmail_StudentEmail_SubjectName

        [JsonPropertyName("teacherEmail")]
        public string TeacherEmail { get; set; } = string.Empty;

        [JsonPropertyName("teacherName")]
        public string TeacherName { get; set; } = string.Empty;

        [JsonPropertyName("subjectName")]
        public string SubjectName { get; set; } = string.Empty;

        [JsonPropertyName("studentEmail")]
        public string StudentEmail { get; set; } = string.Empty;

        [JsonPropertyName("answers")]
        public IDictionary<string, QuestionAnswer> Answers { get; set; } // = new Dictionary<string, QuestionAnswer>();
    }

}
