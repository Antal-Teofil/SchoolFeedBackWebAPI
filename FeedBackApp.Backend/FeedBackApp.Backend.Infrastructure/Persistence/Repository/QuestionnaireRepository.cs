
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
    }
}
