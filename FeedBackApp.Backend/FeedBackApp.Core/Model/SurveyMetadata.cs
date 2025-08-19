
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
        public IList<StudentSet> StudentSets { get; set; } = new List<StudentSet>();

        [JsonPropertyName("questionnaireTemplate")]
        public IList<QuestionTemplate> QuestionnaireTemplate { get; set; } = new List<QuestionTemplate>();

        [JsonPropertyName("teachers")]
        public IList<MetaTeacher> Teachers { get; set; } = new List<MetaTeacher>();

        [JsonPropertyName("questionaireCreationParams")]
        public IList<QuestionnaireCreationParam> CreationParams { get; set; } = new List<QuestionnaireCreationParam>();
    }
}
