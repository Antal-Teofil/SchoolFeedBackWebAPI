namespace Application.DTOs.GetQuestionnairesDTOs
{
    public class GetQuestionnairesResponseDTO
    {
        public string Class { get; set; }
        IList<SubjectDTO> Subjects { get; set; }
    }
}
