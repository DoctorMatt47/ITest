using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.TestAnswers
{
    public class AddTestAnswersByTestQuestionsChoicesCommand : IRequest<IEnumerable<TestAnswer>>
    {
        public IEnumerable<TestAnswerDto> TestAnswerDtos { get; set; }

        public Guid AccountId { get; set; }

        public Test Test { get; set; }

        public class TestAnswerDto
        {
            public string Answer { get; set; }

            public Guid? ChoiceId { get; set; }

            public Guid QuestionId { get; set; }
        }
    }

    public class AddTestAnswersByTestCommandHandler : BaseHandler,
        IRequestHandler<AddTestAnswersByTestQuestionsChoicesCommand, IEnumerable<TestAnswer>>
    {
        private readonly IMapper _mapper;

        public AddTestAnswersByTestCommandHandler(DatabaseContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<TestAnswer>> Handle(AddTestAnswersByTestQuestionsChoicesCommand command,
            CancellationToken cancellationToken)
        {
            var testAnswers =
                command.TestAnswerDtos.Select(dto => _mapper.Map<TestAnswer>(dto)).ToList();

            await _db.TestAnswers.AddRangeAsync(testAnswers, cancellationToken);
            return testAnswers;
        }
    }

    public class AddTestAnswersByTestCommandValidator : AbstractValidator<AddTestAnswersByTestQuestionsChoicesCommand>
    {
        public AddTestAnswersByTestCommandValidator() 
            => RuleFor(cmd => cmd.TestAnswerDtos).NotNull()
                .ForEach(dto => dto.NotNull().SetValidator(new TestAnswerDtoValidator()))
                .Must((cmd, dtos) => dtos.All(ans => cmd.Test.Questions.Select(q => q.Id).Contains(ans.QuestionId)))
                .Must((cmd, dtos) => dtos.All(ans =>
                    cmd.Test.Questions.SelectMany(q => q.Choices.Select(c => c.Id)).Contains(ans.QuestionId)))
                .Must(dtos =>
                {
                    var dtoList = dtos.ToList();
                    return dtoList.Count == dtoList.GroupBy(ans => ans.ChoiceId).Select(g => g.First()).Count();
                })
                .Must(TextQuestionTypeCheck)
                .Must(SingleChoiceQuestionTypeCheck)
                .Must(MultipleChoiceQuestionTypeCheck);


        private static bool TextQuestionTypeCheck(AddTestAnswersByTestQuestionsChoicesCommand cmd,
            IEnumerable<AddTestAnswersByTestQuestionsChoicesCommand.TestAnswerDto> dtos)
            => cmd.Test.Questions
                .Where(question => question.QuestionType == QuestionType.Text)
                .Select(question => dtos.Where(ans => ans.QuestionId == question.Id).ToList())
                .All(answers =>
                    answers.Count == 1
                    && answers.First().ChoiceId is null
                    && answers.First().Answer is not null);

        private static bool SingleChoiceQuestionTypeCheck(AddTestAnswersByTestQuestionsChoicesCommand cmd,
            IEnumerable<AddTestAnswersByTestQuestionsChoicesCommand.TestAnswerDto> dtos)
            => !cmd.Test.Questions
                .Where(q => q.QuestionType == QuestionType.SingleChoice)
                .Select(q => new
                    { question = q, answers = dtos.Where(ans => ans.QuestionId == q.Id).ToList() })
                .Where(t => t.answers.Count != 1)
                .Where(t =>
                {
                    var first = t.answers.First();
                    return !first.ChoiceId.HasValue
                           || !t.question.Choices.Select(c => c.Id).Contains(first.ChoiceId.Value)
                           || first.Answer is not null;
                })
                .Any();

        private static bool MultipleChoiceQuestionTypeCheck(AddTestAnswersByTestQuestionsChoicesCommand cmd,
            IEnumerable<AddTestAnswersByTestQuestionsChoicesCommand.TestAnswerDto> dtos)
            => cmd.Test.Questions
                .Where(q => q.QuestionType == QuestionType.MultipleChoice)
                .All(q => dtos.Where(ans => ans.QuestionId == q.Id)
                    .All(ans =>
                        ans.Answer is null
                        && ans.ChoiceId.HasValue
                        && q.Choices.Select(c => c.Id).Contains(ans.ChoiceId.Value)));


        private class TestAnswerDtoValidator :
            AbstractValidator<AddTestAnswersByTestQuestionsChoicesCommand.TestAnswerDto>
        {
            public TestAnswerDtoValidator()
            {
                RuleFor(ans => ans.Answer).MaximumLength(100);
            }
        }
    }
}