
namespace Application.DTOs
{
    public class MetadataDto
    {
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public List<TeacherDTO> Teachers { get; set; } = new();
        public Dictionary<string, string> Questionnaire { get; set; } = new();
    }
}
