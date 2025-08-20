using Application.DTOs.QuestionnaireDTOs;

namespace Application.Services.Interfaces
{
    public interface IQuestionnaireService
    {
        public Task<CreationResponseDTO> CompileAndSaveAsync(CreateSurveyMetadataDto dto);
    }
}
