using Application.DTOs.QuestionnaireDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class MetaTeacherValidator : AbstractValidator<MetaTeacherDto>
    {
        public MetaTeacherValidator() 
        {
            RuleFor(dto => dto.Email).NotEmpty().WithMessage("Teacher email can not be empty")
                .EmailAddress().WithMessage("Incorrect teacher email adress format");
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Teacher name can not be empty")
                .MaximumLength(100).WithMessage("Teacher name can not exceed 100 characters");
        }
    }
}
