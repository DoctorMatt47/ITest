using FluentValidation;
using ITest.Data.Dtos.Tests;

namespace ITest.Data.Validators
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
                .Must(l => l.Count < 50);

            RuleForEach(q => q.Choices)
                .SetValidator(new ChoiceValidator());
        }
    }
}