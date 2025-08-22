﻿
using Application.DTOs.QuestionnaireDTOs;
using FluentValidation;

namespace Application.Validation
{
    public class CreateSurveyMetadataValidator : AbstractValidator<CreateSurveyMetadataDto>
    {
        public CreateSurveyMetadataValidator() 
        {
            RuleFor(dto => dto.StartDate).NotEmpty().WithMessage("Start date can not be empty")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Start date can not be in the future. Found: {PropertyValue}");
            RuleFor(dto => dto.EndDate).NotEmpty().WithMessage("End date can not be empty")
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("End date must be a future date. Found: {PropertyValue}");
            RuleFor(dto => dto.StudentSets).NotEmpty().WithMessage("Student sets can not be empty");
            RuleForEach(dto => dto.StudentSets)
                .SetValidator(new StudentSetValidator());
            RuleFor(dto => dto.QuestionTemplates).NotEmpty().WithMessage("Question templates must be provided");
            RuleForEach(dto => dto.QuestionTemplates)
                .SetValidator(new QuestionTemplateValidator());
            RuleFor(dto => dto.Teachers).NotEmpty().WithMessage("Teacher list can not be empty");
            RuleForEach(dto => dto.Teachers)
                .SetValidator(new MetaTeacherValidator());
            RuleFor(dto => dto.CreationParams).NotEmpty().WithMessage("Creation params must be specified");
            RuleForEach(dto => dto.CreationParams)
                .SetValidator(new QuestionnaireCreationParamValidator());
        }
    }
}
