﻿using Application.DTOs.QuestionnaireDTOs;
using FeedBackApp.Core.Model.Enum;
using FluentValidation;

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
            When(dto => dto.Type == QuestionType.MultinomialSingleChoice
                     || dto.Type == QuestionType.MultipleChoice, () =>
                     {
                         RuleFor(dto => dto.AnswerOptions)
                             .NotNull().WithMessage("Answer options are required for single- or multiple-choice questions questions")
                             .Must(options => options != null && options.Any(o => !string.IsNullOrWhiteSpace(o)))
                                 .WithMessage("Answer options must contain at least one non-empty option for single- or multiple-choice questions")
                             .Must(options => options!.All(o => !string.IsNullOrWhiteSpace(o)))
                                 .WithMessage("Answer options cannot contain empty values for single- or multiple-choice questions");
                     });
        }
    }
}
