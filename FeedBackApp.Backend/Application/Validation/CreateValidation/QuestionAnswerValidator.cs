using Application.DTOs.Questionnaire;
using FluentValidation;

namespace Application.Validation.CreateValidation
{
    public class QuestionAnswerValidator : AbstractValidator<QuestionAnswerDTO>
    {
        public QuestionAnswerValidator()
        {
        }
    }
}
