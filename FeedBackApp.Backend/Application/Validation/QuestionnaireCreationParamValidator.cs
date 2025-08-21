using Application.DTOs.QuestionnaireDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class QuestionnaireCreationParamValidator : AbstractValidator<QuestionnaireCreationParamDto>
    {
        public QuestionnaireCreationParamValidator()
        {
            RuleFor(dto=> dto.TeacherEmail)
                .NotEmpty().WithMessage("Teacher email cannot be empty")
                .EmailAddress().WithMessage("Invalid teacher email format");

            RuleFor(dto=> dto.SubjectName)
                .NotEmpty().WithMessage("Subject name cannot be empty")
                .MaximumLength(200).WithMessage("Subject name cannot exceed 200 characters");

            RuleFor(dto=> dto.StudentSetIds)
                .NotEmpty().WithMessage("At least one student set ID must be provided");

            RuleForEach(dto=> dto.StudentSetIds)
                .NotEmpty().WithMessage("Student set ID cannot be empty");

        }

    }
}
