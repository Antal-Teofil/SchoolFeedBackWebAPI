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
        public async Task<DeletionResponseDTO> DeleteSurveyAsync(Guid id)
        {
            try
            {
                bool surveyDeleted = await _repository.DeleteSurveyMetadataAsync(id);
                bool questionnairesDeleted = await _repository.DeleteQuestionnairesBySurveyIdAsync(id);

                if (surveyDeleted || questionnairesDeleted)
                {
                    return new DeletionResponseDTO
                    {
                        Success = true,
                        Message = $"Survey {id} and related questionnaires were deleted successfully."
                    };
                }
                else
                {
                    return new DeletionResponseDTO
                    {
                        Success = false,
                        Message = $"Survey {id} not found (no survey metadata or questionnaires)."
                    };
                }
            }
            catch (Exception ex)
            {
                return new DeletionResponseDTO
                {
                    Success = false,
                    Message = $"Error deleting survey {id}: {ex.Message}"
                };
            }
        }
    }
}
