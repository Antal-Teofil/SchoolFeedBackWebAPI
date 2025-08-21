
using Newtonsoft.Json;
namespace FeedBackApp.Core.Model
{
    public class StudentSet
    {
        public string SetId { get; set; } = string.Empty;

        public IList<string> StudentEmails { get; set; } = new List<string>();
    }
}
