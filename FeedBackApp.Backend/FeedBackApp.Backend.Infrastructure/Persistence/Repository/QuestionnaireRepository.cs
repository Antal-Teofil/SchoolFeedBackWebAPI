
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

        public async Task<bool> CompileAndSaveAsync(SurveyMetadata metadata)
        {
            try
            {
                _context.Add(metadata);
                await _context.SaveChangesAsync();

                foreach (var param in metadata.)
                {
                    
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
