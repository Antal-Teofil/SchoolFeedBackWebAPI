using Application.DTOs.Questionnaire;
using FeedBackApp.Core.Model;
using FeedBackApp.Core.Model.Enum;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.UpdateValidation
{
    public class QuestionResultValidator : AbstractValidator<QuestionResultDTO>
    {
        public QuestionResultValidator(IList<QuestionTemplate> templates)
        {

            RuleFor(dto => dto.QuestionId)
                .NotEmpty().WithMessage("QuestionId cannot be null/empty")
                .Must(id => templates.Any(t => t.Id == id))
                .WithMessage(dto => $"Question with id {dto.QuestionId} does not exist.");

            RuleFor(dto => dto.Answer)
                .Custom((answer, context) =>
                {
                    var dtoInstance = (QuestionResultDTO)context.InstanceToValidate;

                    var template = templates.FirstOrDefault(t => t.Id == dtoInstance.QuestionId);
                    if (template == null)
                        return;

                    switch (template.Type)
                    {
                        case QuestionType.OpenEnded:
                            if (string.IsNullOrWhiteSpace(answer))
                                context.AddFailure("Answer", $"Answer cannot be empty for '{template.Question}'.");
                            break;

                        case QuestionType.MultinomialSingleChoice:
                            if (!Regex.IsMatch(answer, @"^\d+$"))
                            {
                                context.AddFailure("Answer", $"Answer must be a number for '{template.Question}'.");
                            }
                            break;

                        case QuestionType.MultipleChoice:
                            if (!Regex.IsMatch(answer, @"^\d+(?:-\d+)*$"))
                            {
                                context.AddFailure("Answer", $"Answer must be numbers separated by '-' for '{template.Question}'.");
                            }

                            break;

                        case QuestionType.LikertScaleOneToFive:
                            if (!Regex.IsMatch(answer, @"^[0-5]$"))
                            {
                                context.AddFailure("Answer", $"Answer must be a number between 0 and 5 for '{template.Question}'.");
                            }
                            break;

                        default:
                            break;
                    }
                });
        }
    }
}
