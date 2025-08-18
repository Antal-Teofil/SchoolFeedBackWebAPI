
namespace FeedBackApp.Core.Model
{
    public class Metadata
    {
        // [JsonPropertyName("id")] ?
        public string Id { get; set; } = string.Empty; // Cosmos requires id

        public List<MetaTeacher> Teachers { get; set; } = new();

        public string StartDate { get; set; } = string.Empty;

        public string EndDate { get; set; } = string.Empty;

        public Dictionary<string, string> Questionnaire { get; set; } = new();
    }
}
