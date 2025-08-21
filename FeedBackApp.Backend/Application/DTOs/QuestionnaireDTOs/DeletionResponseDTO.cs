
namespace Application.DTOs.QuestionnaireDTOs
{
    public class DeletionResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }=string.Empty;

        public DeletionResponseDTO(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
