
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class SurveyMetadata
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("startDate")]
        public string StartDate { get; set; } = string.Empty;

        [JsonPropertyName("endDate")]
        public string EndDate { get; set; } = string.Empty;

        [JsonPropertyName("studentSets")]
        public List<StudentSet> StudentSets { get; set; } = new();

        [JsonPropertyName("questionnaireTemplate")]
        public List<QuestionTemplate> QuestionnaireTemplate { get; set; } = new();

        [JsonPropertyName("teachers")]
        public List<MetaTeacher> Teachers { get; set; } = new();

        [JsonPropertyName("questionaireCreationParams")]
        public List<QuestionnaireCreationParam> CreationParams { get; set; } = new();
    }
}
