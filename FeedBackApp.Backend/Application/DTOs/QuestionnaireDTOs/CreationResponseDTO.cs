namespace Application.DTOs.QuestionnaireDTOs
{
    public class CreationResponseDTO
    {
        public bool Success { get; set; }
        
        public string Message { get; set; }
        public CreationResponseDTO(bool success)
        {
            Success = success;
        }
    }
}
