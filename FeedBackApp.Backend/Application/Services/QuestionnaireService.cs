using Application.DTOs;
using Application.Services.Interfaces;
using FeedBackApp.Core.Model;
using FeedBackApp.Core.Model.Enum;
using FeedBackApp.Core.Repositories;
using Newtonsoft.Json.Linq;

namespace Application.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _repository;
        public QuestionnaireService(IQuestionnaireRepository repository)
        {
            _repository = repository;
        }

        public async Task ProcessMetadataAsync(MetadataDto dto)
        {
            var metadata = new Metadata
            {
                Id = Guid.NewGuid().ToString(),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Teachers = dto.Teachers.Select(t => new MetaTeacher
                {
                    Email = t.Email,
                    Name = t.Name,
                    Subjects = t.Subjects.Select(s => new MetaSubject
                    {
                        Name = s.Name,
                        Students = s.Students
                    }).ToList()
                }).ToList(),
                Questionnaire = dto.Questionnaire
            };

            await _repository.SaveMetadataAsync(metadata);

            foreach (var teacher in metadata.Teachers)
            {
                foreach (var subject in teacher.Subjects)
                {
                    foreach (var student in subject.Students)
                    {
                        var questionnaire = new Questionnaire
                        {
                            Id = $"qnr_{student}_{teacher.Email}_{subject.Name}",
                            TeacherEmail = teacher.Email,
                            TeacherName = teacher.Name,
                            SubjectName = subject.Name,
                            StudentEmail = student,
                            Answers = metadata.Questionnaire.ToDictionary(
                                q => q.Key,
                                q => new QuestionAnswer { Type = (QuestionType)Enum.Parse(typeof(QuestionType), q.Value) } // hmmm
                            )
                        };

                        await _repository.SaveQuestionnaireAsync(questionnaire);
                    }
                }
            }
        }
    }
}
