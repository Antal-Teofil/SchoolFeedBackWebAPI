
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class MetaTeacher
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
