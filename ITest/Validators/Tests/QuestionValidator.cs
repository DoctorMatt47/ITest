using FluentValidation;
using ITest.Data.Dtos.Tests;
using ITest.Data.Entities.Tests;

namespace ITest.Validators.Tests
{
    public class QuestionValidator : AbstractValidator<QuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.QuestionString).NotNull()
                .Length(3, 255);

            RuleFor(q => q.QuestionType)
                .IsInEnum();
            
            RuleFor(q => q.Choices)
                .Must(l => l.Count == 0)
                .When(q => q.QuestionType == QuestionType.Text);

            RuleFor(q => q.Choices)
                .Must(l => l.Count <= 50)
                .When(q => q.QuestionType != QuestionType.Text);

            RuleForEach(q => q.Choices)
                .SetValidator(new ChoiceValidator());
        }
    }
}