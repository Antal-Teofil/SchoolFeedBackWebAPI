using Application.DTOs.Questionnaire;
using FeedBackApp.Core.Model;
using FluentValidation;

namespace Application.Validation.UpdateValidation
{
    public class UpdateQuestionnaireValidator : AbstractValidator<UpdateQuestionnaireDTO>
    {
        public UpdateQuestionnaireValidator(QuestionnaireTemplate template)
        {
            RuleForEach(dto => dto.QuestionnaireResult)
                .SetValidator(new QuestionResultValidator(template.QuestionTemplates));
        }
    }
}
