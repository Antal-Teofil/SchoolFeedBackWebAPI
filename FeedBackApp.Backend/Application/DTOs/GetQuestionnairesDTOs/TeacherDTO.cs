
namespace Application.DTOs.GetQuestionnairesDTOs
{
    public class TeacherDTO
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
    }
}
