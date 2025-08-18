
using System.ComponentModel.DataAnnotations;

namespace FeedBackApp.Core.Model
{
    public class Questionnaire
    {
        public string Id { get; set; } = string.Empty; 
        public string SurveyId { get; set; } = string.Empty; // is this neccessary?
        public Dictionary<string, QuestionAnswer> Answers { get; set; } = new();
    }
}
