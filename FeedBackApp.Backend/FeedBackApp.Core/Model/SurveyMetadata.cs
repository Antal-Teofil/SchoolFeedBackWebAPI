

using Newtonsoft.Json;

namespace FeedBackApp.Core.Model
{
    public class SurveyMetadata
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("startDate")]
        public string StartDate { get; set; } = string.Empty;

        [JsonProperty("endDate")]
        public string EndDate { get; set; } = string.Empty;

        [JsonProperty("studentSets")]
        public IList<StudentSet> StudentSets { get; set; } = new List<StudentSet>();

        [JsonProperty("questionnaireTemplate")]
        public IList<QuestionTemplate> QuestionnaireTemplate { get; set; } = new List<QuestionTemplate>();

        [JsonProperty("teachers")]
        public IList<MetaTeacher> Teachers { get; set; } = new List<MetaTeacher>();

        [JsonProperty("questionaireCreationParams")]
        public IList<QuestionnaireCreationParam> QuestionaireCreationParams { get; set; } = new List<QuestionnaireCreationParam>();
    }
}
