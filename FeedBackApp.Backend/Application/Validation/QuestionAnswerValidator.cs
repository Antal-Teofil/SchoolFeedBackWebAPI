using Application.DTOs.QuestionnaireDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class QuestionAnswerValidator : AbstractValidator<QuestionAnswerDto>
    {
        public QuestionAnswerValidator()
        {
            RuleFor(a => a.Question)
                .NotEmpty().WithMessage("Question text cannot be empty")
                .MaximumLength(500).WithMessage("Question text cannot exceed 500 characters");

            RuleFor(a => a.Type)
                .IsInEnum().WithMessage("Invalid question type");


            When(a => a.Type != FeedBackApp.Core.Model.Enum.QuestionType.OpenEnded, () =>
            {
                RuleFor(a => a.Answer)
                    .NotEmpty().WithMessage("Answer is required for this question type");
            });
        }
    }
}
