using Application.DTOs.Questionnaire;
using FluentValidation;

namespace Application.Validation.UpdateValidation
{
    public class UpdateQuestionnaireValidator : AbstractValidator<UpdateQuestionnaireDTO>
    {
        public UpdateQuestionnaireValidator()
        {
            RuleForEach(dto => dto.QuestionnaireResult)
                .SetValidator(new QuestionResultValidator());
        }
    }
}
