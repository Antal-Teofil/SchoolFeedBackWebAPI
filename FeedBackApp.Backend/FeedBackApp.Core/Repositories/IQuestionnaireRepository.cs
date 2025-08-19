
using FeedBackApp.Core.Model;

namespace FeedBackApp.Core.Repositories
{
    public interface IQuestionnaireRepository
    {
        Task SaveMetadataAsync(Metadata metadata);
        Task SaveQuestionnaireAsync(Questionnaire questionnaire);
        Task<Metadata?> GetMetadataAsync(string id);
        Task<Questionnaire?> GetQuestionnaireAsync(string id);
    }
}
