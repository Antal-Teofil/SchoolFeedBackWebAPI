
using FeedBackApp.Core.Model;
using FeedBackApp.Core.Repositories;

namespace FeedBackApp.Backend.Infrastructure.Persistence.Repository
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly AppDBContext _context;

        public QuestionnaireRepository(AppDBContext context)
        {
            _context = context;
        }

        public Task<Metadata?> GetMetadataAsync(string id)
        {
            return await
        }

        public Task<Questionnaire?> GetQuestionnaireAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task SaveMetadataAsync(Metadata metadata)
        {
            throw new NotImplementedException();
        }

        public Task SaveQuestionnaireAsync(Questionnaire questionnaire)
        {
            throw new NotImplementedException();
        }
    }
}
