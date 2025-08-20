
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

                var setById = metadata.StudentSets.ToDictionary(s => s.SetId);
                var template = metadata.QuestionnaireTemplate;

                var questionnaires = new List<Questionnaire>();

                foreach (var param in metadata.CreationParams)
                {
                    foreach (var setId in param.StudentSetIds)
                    {
                        var set = setById[setId];

                        foreach (var studentEmail in set.StudentEmails)
                        {
                            var q = new Questionnaire
                            {
                                Id = Guid.NewGuid().ToString(),
                                SurveyId = metadata.Id,
                                TeacherEmail = param.TeacherEmail,
                                StudentEmail = studentEmail,
                                SubjectName = param.SubjectName,
                                // PartitionKey = $"{studentEmail}_{param.TeacherEmail}_{param.SubjectName}",
                                QuestionnaireResults = template
                                    .Select(t => new QuestionAnswer
                                    {
                                        Question = t.Question,
                                        Type = t.Type,
                                        Answer = null
                                    })
                                    .ToList()
                            };

                            questionnaires.Add(q);
                        }
                    }
                }

                if (questionnaires.Count > 0)
                {
                    _context.AddRange(questionnaires);
                    await _context.SaveChangesAsync();
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
