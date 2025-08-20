using Application.DTOs.QuestionnaireDTOs;
using Application.Services.Interfaces;
using FeedBackApp.Core.Repositories;

namespace Application.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _repository;
        public QuestionnaireService(IQuestionnaireRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreationResponseDTO> CompileAndSaveAsync(CreateSurveyMetadataDto dto)
        {
            dto.ToModel();
            return new CreationResponseDTO(await _repository.CompileAndSaveAsync(dto));
        }
    }
}
