using Application.DTOs.QuestionnaireDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class QuestionTemplateValidator : AbstractValidator<QuestionTemplateDto>
    {
        public QuestionTemplateValidator() 
        {
            RuleFor(dto => dto.Question).NotEmpty().WithMessage("Question text can not be empty")
                .MaximumLength(500).WithMessage("Question can not be longer than 500 characters: {PropertyValue}");
            RuleFor(dto => dto.Type)
                .NotNull().WithMessage("Question type is required")
                .IsInEnum().WithMessage("Invalid question type: {PropertyValue}");
        }
    }
}
