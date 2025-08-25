using Application.DTOs.Questionnaire;
using FluentValidation;

namespace Application.Validation
{
    public class QuestionAnswerValidator : AbstractValidator<QuestionAnswerDTO>
    {
        public QuestionAnswerValidator()
        {
        }
    }
}
