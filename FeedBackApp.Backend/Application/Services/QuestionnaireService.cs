using Application.DTOs.QuestionnaireDTOs;
using Application.Extensions.QuestionnaireExtensions;
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
            var metadata = dto.ToModel();
            if(await _repository.CompileAndSaveAsync(metadata))
            {
                return new CreationResponseDTO(true, "Creation successful!");
            }
            return new CreationResponseDTO(false, "Creation failed!");
        }
    }
}
