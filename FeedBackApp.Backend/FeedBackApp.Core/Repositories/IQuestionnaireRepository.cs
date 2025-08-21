
using FeedBackApp.Core.Model;

namespace FeedBackApp.Core.Repositories
{
    public interface IQuestionnaireRepository
    {
        public Task CompileAndSaveAsync(SurveyMetadata metadata);
        Task<bool> DeleteSurveyMetadataAsync(Guid id);
        Task<bool> DeleteQuestionnairesBySurveyIdAsync(Guid surveyId);
    }
}
