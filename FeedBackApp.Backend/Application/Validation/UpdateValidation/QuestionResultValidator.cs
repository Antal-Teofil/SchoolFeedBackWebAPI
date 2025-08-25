using Application.DTOs.Questionnaire;
using FluentValidation;

namespace Application.Validation.UpdateValidation
{
    public class QuestionResultValidator : AbstractValidator<QuestionResultDTO>
    {
        public QuestionResultValidator()
        {
            RuleFor(dto => dto.QuestionId).NotEmpty().NotNull().WithMessage("QuestionId cannot be null/empty");
            RuleFor(dto => dto.answ)
        }
    }
}
