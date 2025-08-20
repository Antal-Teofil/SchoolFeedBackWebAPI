
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class Questionnaire
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("surveyId")]
        public string SurveyId { get; set; } = string.Empty;

        [JsonPropertyName("teacherEmail")]
        public string TeacherEmail { get; set; } = string.Empty;

        [JsonPropertyName("studentEmail")]
        public string StudentEmail { get; set; } = string.Empty;

        [JsonPropertyName("subjectName")]
        public string SubjectName { get; set; } = string.Empty;

        // partition key = student+teacher+subject
        [JsonPropertyName("partitionKey")]
        public string PartitionKey => $"{StudentEmail}_{TeacherEmail}_{SubjectName}" {get;set;} // freely modifyable

        [JsonPropertyName("questionnaire")]
        public IList<QuestionAnswer> QuestionnaireResults { get; set; } = new List<QuestionAnswer>();
    }
}
