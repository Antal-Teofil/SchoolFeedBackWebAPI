
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

        public async Task<Metadata?> GetMetadataAsync(string id)
        {
            return await _context.Set<Metadata>().FindAsync(id);
        }

        public async Task<Questionnaire?> GetQuestionnaireAsync(string id)
        {
            return await _context.Set<Questionnaire>().FindAsync(id);
        }

        public async Task SaveMetadataAsync(Metadata metadata)
        {
            _context.Set<Metadata>().Add(metadata);
            await _context.SaveChangesAsync();
        }

        public async Task SaveQuestionnaireAsync(Questionnaire questionnaire)
        {
            _context.Set<Questionnaire>().Add(questionnaire);
            await _context.SaveChangesAsync();
        }
    }
}
