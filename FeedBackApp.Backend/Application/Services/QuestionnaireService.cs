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
    }
}
