
namespace FeedBackApp.Core.Model
{
    public class MetaTeacher
    {
        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<MetaSubject> Subjects { get; set; } = new();
    }
}
