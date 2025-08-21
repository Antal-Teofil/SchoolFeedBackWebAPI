
using FeedBackApp.Core.Model;

namespace FeedBackApp.Core.Repositories
{
    public interface IQuestionnaireRepository
    {
        public Task<bool> CompileAndSaveAsync(SurveyMetadata metadata);
        Task<bool> DeleteSurveyMetadataAsync(Guid id);
        Task<bool> DeleteQuestionnairesBySurveyIdAsync(Guid surveyId);
    }
}
