using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITest.Configs;
using ITest.Cqrs.Choices;
using ITest.Cqrs.Questions;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class AddTestQuestionsChoicesCommand : IRequest<Test>
    {
        public Guid AccountId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<QuestionChoicesDto> Questions { get; set; }
            = new List<QuestionChoicesDto>();

        public class QuestionChoicesDto
        {
            public string QuestionString { get; set; }

            public QuestionType QuestionType { get; set; }

            public List<ChoiceDto> Choices { get; set; } =
                new List<ChoiceDto>();

            public class ChoiceDto
            {
                public string ChoiceString { get; set; }
            }
        }
    }

    public class AddTestQuestionsChoicesCommandHandler : BaseHandler,
        IRequestHandler<AddTestQuestionsChoicesCommand, Test>
    {
        private readonly IMapper _mapper;

        public AddTestQuestionsChoicesCommandHandler(DatabaseContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<Test> Handle(AddTestQuestionsChoicesCommand command,
            CancellationToken cancellationToken)
        {
            var newTest = _mapper.Map<Test>(command);
            newTest.AccountId = command.AccountId;

            await _db.Tests.AddAsync(newTest, cancellationToken);
            foreach (var question in newTest.Questions)
            {
                await _db.Questions.AddAsync(question, cancellationToken);
                foreach (var choice in question.Choices)
                {
                    await _db.Choices.AddAsync(choice, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);

            return newTest;
        }
    }

    public class AddTestQuestionsChoicesCommandValidator :
        AbstractValidator<AddTestQuestionsChoicesCommand>
    {
        public AddTestQuestionsChoicesCommandValidator()
        {
            RuleFor(t => t.Title).NotNull()
                .Length(3, 100).Matches(RegularExpression.TestTitle);

            RuleFor(t => t.Description).NotNull()
                .Length(3, 500).Matches(RegularExpression.TestDescription);

            RuleFor(t => t.Questions).NotNull()
                .Must(l => l.Count > 0 && l.Count <= 100);

            RuleForEach(t => t.Questions).NotNull()
                .SetValidator(new QuestionChoicesDtoValidator());
        }

        private class QuestionChoicesDtoValidator :
            AbstractValidator<AddTestQuestionsChoicesCommand.QuestionChoicesDto>
        {
            public QuestionChoicesDtoValidator()
            {
                RuleFor(q => q.QuestionString).NotNull()
                    .Length(3, 255).Matches(RegularExpression.TestQuestionString);

                RuleFor(q => q.QuestionType).IsInEnum();

                RuleFor(q => q.Choices).NotNull()
                    .Must(l => l.Count == 0)
                    .When(q => q.QuestionType == QuestionType.Text);

                RuleFor(q => q.Choices).NotNull()
                    .Must(l => l.Count <= 50)
                    .When(q => q.QuestionType != QuestionType.Text);


                RuleForEach(t => t.Choices).NotNull()
                    .SetValidator(new ChoiceDtoValidator());
            }

            private class ChoiceDtoValidator :
                AbstractValidator<AddTestQuestionsChoicesCommand.QuestionChoicesDto.ChoiceDto>
            {
                public ChoiceDtoValidator()
                {
                    RuleFor(t => t.ChoiceString).NotNull()
                        .Length(1, 100).Matches(RegularExpression.TestChoiceString);
                }
            }
        }
    }
    
    public class AddTestQuestionsChoicesCommandProfile : Profile
    {
        public AddTestQuestionsChoicesCommandProfile()
        {
            CreateMap<AddTestQuestionsChoicesCommand, Test>();
        }
    }
}