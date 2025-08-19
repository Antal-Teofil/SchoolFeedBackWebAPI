
using System.Text.Json.Serialization;

namespace FeedBackApp.Core.Model
{
    public class StudentSet
    {
        [JsonPropertyName("setId")]
        public string SetId { get; set; } = string.Empty;

        [JsonPropertyName("studentEmails")]
        public List<string> StudentEmails { get; set; } = new();
    }
}
