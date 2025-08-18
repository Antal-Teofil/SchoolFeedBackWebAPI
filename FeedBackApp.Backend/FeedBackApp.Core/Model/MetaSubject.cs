
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class MetaSubject
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("students")]
        public List<string> Students { get; set; } = new();
    }
}
