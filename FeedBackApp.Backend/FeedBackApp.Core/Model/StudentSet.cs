

using Newtonsoft.Json;

namespace FeedBackApp.Core.Model
{
    public class StudentSet
    {
        [JsonProperty("setId")]
        public string SetId { get; set; } = string.Empty;

        [JsonProperty("studentEmails")]
        public IList<string> StudentEmails { get; set; } = new List<string>();
    }
}
