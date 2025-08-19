
namespace Application.DTOs
{
    public class TeacherDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<SubjectDTO> Subjects { get; set; } = new();
    }
}
