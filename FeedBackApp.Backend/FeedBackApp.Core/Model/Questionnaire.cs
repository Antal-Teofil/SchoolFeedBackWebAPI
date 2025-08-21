
using Newtonsoft.Json;

namespace FeedBackApp.Core.Model
{
    public class Questionnaire
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("surveyId")]
        public string SurveyId { get; set; } = string.Empty;

        [JsonProperty("teacherEmail")]
        public string TeacherEmail { get; set; } = string.Empty;

        [JsonProperty("studentEmail")]
        public string StudentEmail { get; set; } = string.Empty;

        [JsonProperty("subjectName")]
        public string SubjectName { get; set; } = string.Empty;

        [JsonProperty("questionnaire")]
        public IList<QuestionAnswer> QuestionnaireResults { get; set; } = new List<QuestionAnswer>();
    }
}
