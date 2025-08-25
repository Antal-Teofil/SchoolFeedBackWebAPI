namespace Application.DTOs.GetQuestionnairesDTOs
{
    public class GetQuestionnairesResponseDTO
    {
        public string @Class { get; set; }
        public List<SubjectDTO> Subjects { get; set; }
    }
}
