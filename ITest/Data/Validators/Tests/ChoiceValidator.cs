using FluentValidation;
using ITest.Data.Dtos.Tests;

namespace ITest.Data.Validators
{
    public class ChoiceValidator: AbstractValidator<ChoiceDto>
    {
        public ChoiceValidator()
        {
            RuleFor(c => c.ChoiceString).NotNull()
                .MaximumLength(100);
        }
    }
}