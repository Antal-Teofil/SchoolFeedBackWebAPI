
namespace Application.DTOs
{
    public class SubjectDto
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Students { get; set; } = new();
    }
}
