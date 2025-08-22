using Application.DTOs.QuestionnaireDTOs;
using FluentValidation;

namespace Application.Validation
{
    public class QuestionAnswerValidator : AbstractValidator<QuestionAnswerDto>
    {
        public QuestionAnswerValidator()
        {
        }
    }
}
