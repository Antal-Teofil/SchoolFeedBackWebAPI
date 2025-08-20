
using FeedBackApp.Core.Model;

namespace FeedBackApp.Core.Repositories
{
    public interface IQuestionnaireRepository
    {
        public Task<bool> CompileAndSaveAsync(SurveyMetadata metadata);
    }
}
