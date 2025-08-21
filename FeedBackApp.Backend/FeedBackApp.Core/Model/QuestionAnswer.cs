
using FeedBackApp.Core.Model.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FeedBackApp.Core.Model
{
    public class QuestionAnswer
    {
        [JsonProperty("question")]
        public string Question { get; set; } = string.Empty;

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public QuestionType Type { get; set; }

        [JsonProperty("answer")]
        public string? Answer { get; set; } = null;
    }
}
