
namespace FeedBackApp.Core.Model
{
    public class ReviewIndex
    {
        public string Id { get; set; } = string.Empty;

        public string Type { get; set; } = "reviewIndex";

        public string SurveyId { get; set; } = string.Empty; // is this neccessary?

        public List<ReviewEntry> Entries { get; set; } = new();
    }
}
