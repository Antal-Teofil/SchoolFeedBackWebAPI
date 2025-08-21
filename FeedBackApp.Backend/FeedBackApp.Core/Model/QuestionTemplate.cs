
using FeedBackApp.Core.Model.Enum;
using Newtonsoft.Json;

namespace FeedBackApp.Core.Model
{
    public class QuestionTemplate
    {
        [JsonProperty("question")]
        public string Question { get; set; } = string.Empty;

        [JsonProperty("type")]
        public QuestionType Type { get; set; }
    }
}
