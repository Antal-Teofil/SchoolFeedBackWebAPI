
using Newtonsoft.Json;

namespace FeedBackApp.Core.Model
{
    public class QuestionnaireCreationParam
    {
        [JsonProperty("teacherEmail")]
        public string TeacherEmail { get; set; } = string.Empty;

        [JsonProperty("subjectName")]
        public string SubjectName { get; set; } = string.Empty;

        [JsonProperty("studentSetId")]
        public IList<string> StudentSetIds { get; set; } = new List<string>();
    }
}
