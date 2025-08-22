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
            try
            {
                await _repository.CompileAndSaveAsync(metadata);
            
                return new CreationResponseDTO(true, "Creation successful!");
            }
            catch (Exception e)
            {
                return new CreationResponseDTO(false, $"Creation failed: {e.Message}");
            }
            
        }
        public async Task<DeletionResponseDTO> DeleteSurveyAsync(Guid id)
        {
            try
            {
                bool surveyDeleted = await _repository.DeleteSurveyMetadataAsync(id);
                bool questionnairesDeleted = await _repository.DeleteQuestionnairesBySurveyIdAsync(id);
                bool questionTemplateDeleted = await _repository.DeleteQuestionTemplateBySurveyIdAsync(id);

                if (surveyDeleted || questionnairesDeleted || questionTemplateDeleted)
                {
                    return new DeletionResponseDTO
                    (
                        true,
                       $"Survey {id} and related questionnaires were deleted successfully."
                    );
                }
                else
                {
                    return new DeletionResponseDTO
                    (
                        false,
                        $"Survey {id} not found (no survey metadata or questionnaires)."
                    );
                }
            }
            catch (Exception ex)
            {
                return new DeletionResponseDTO
                (
                    false,
                    $"Error deleting survey {id}: {ex.Message}"
                );
            }
        }
    }
}
