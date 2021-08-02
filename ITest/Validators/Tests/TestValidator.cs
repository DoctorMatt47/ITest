using System.Text.RegularExpressions;
using FluentValidation;
using ITest.Data.Dtos.Tests;

namespace ITest.Validators.Tests
{
    public class TestValidator : AbstractValidator<TestDto>
    {
        private readonly IValidator<QuestionDto> _validator;

        public TestValidator(IValidator<QuestionDto> validator)
        {
            _validator = validator;
            
            RuleFor(t => t.Title).NotNull()
                .Length(3, 100).Must(IsTitleValid);

            RuleFor(t => t.Description).NotNull()
                .Length(3, 500);

            RuleFor(t => t.Questions).NotNull()
                .Must(l => l.Count > 0 && l.Count <= 100);

            RuleForEach(t => t.Questions)
                .SetValidator(_validator);
        }

        private static bool IsTitleValid(string t)
        {
            const string regex = "^[a-zA-Z0-9!? ]*$";
            return Regex.IsMatch(t, regex, RegexOptions.IgnoreCase);
        }
    }
}