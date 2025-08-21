
using Newtonsoft.Json;

namespace FeedBackApp.Core.Model
{
    public class MetaTeacher
    {
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
