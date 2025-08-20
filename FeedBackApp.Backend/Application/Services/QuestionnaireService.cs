using Application.DTOs.QuestionnaireDTOs;
using Application.Services.Interfaces;
using FeedBackApp.Backend.Infrastructure.Persistence.Repository;
using FeedBackApp.Core.Model;
using FeedBackApp.Core.Model.Enum;
using FeedBackApp.Core.Repositories;
using Newtonsoft.Json.Linq;

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

    public static class ValamiExtension
    {
        public static SurveyMetadata ToModel(this CreateSurveyMetadataDto dto) =>
            new()
            {
                CreationParams = dto.CreationParams.ToModel()
            };
    }
}
