using Application.DTOs.QuestionnaireDTOs;
using FluentValidation;

namespace Application.Validation
{
    public class StudentSetValidator : AbstractValidator<StudentSetDto>
    {
        public StudentSetValidator() 
        {
            RuleFor(dto => dto.SetId).NotEmpty().WithMessage("Studentset needs an ID");
            RuleFor(dto => dto.StudentEmails).NotEmpty().WithMessage("Student email list can not be empty");
            RuleForEach(dto => dto.StudentEmails).EmailAddress().WithMessage("Invalid email adress format");
        }
    }
}
