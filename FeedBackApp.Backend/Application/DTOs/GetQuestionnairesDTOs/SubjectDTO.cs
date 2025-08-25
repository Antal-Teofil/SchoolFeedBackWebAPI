
namespace Application.DTOs.GetQuestionnairesDTOs
{
    public class SubjectDTO
    {
        public string Name { get; set; }
        public List<TeacherDTO> Teachers { get; set; } = new List<TeacherDTO>();
    }
}
