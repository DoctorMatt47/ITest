using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using ITest.Cqrs.Accounts;
using ITest.Cqrs.Tests;
using ITest.Data;
using ITest.Data.Dtos.TestAnswers;
using ITest.Data.Entities.Tests;
using ITest.Exceptions;
using ITest.Exceptions.Tests;
using MediatR;

namespace ITest.Cqrs.TestAnswers
{
    public class AddTestAnswersCommandHandler : BaseHandler,
        IRequestHandler<AddTestAnswersCommand, IEnumerable<TestAnswer>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddTestAnswersCommandHandler(DatabaseContext db, IMediator mediator, IMapper mapper) : base(db)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TestAnswer>> Handle(AddTestAnswersCommand command,
            CancellationToken cancellationToken)
        {
            var getTestQuery = new GetTestQuestionsChoicesByTestIdQuery(command.TestId);
            var testToAnswer = await _mediator.Send(getTestQuery, cancellationToken);

            var fixedTestAnswers = 
                TestAnswersFixAndValidate(testToAnswer, command.TestAnswerDtos);

            var testAnswers =
                fixedTestAnswers.Select(dto => _mapper.Map<TestAnswer>(dto)).ToList();

            await _db.TestAnswers.AddRangeAsync(testAnswers, cancellationToken);
            return testAnswers;
        }

        private static IEnumerable<TestAnswerDto> TestAnswersFixAndValidate(Test testToAnswer,
            IEnumerable<TestAnswerDto> testAnswerDtos)
        {
            if (testToAnswer is null)
            {
                throw new TestNotFoundException("Test with this id was not found.");
            }

            var questionIds = testToAnswer.Questions.Select(q => q.Id);
            var answerDtos = testAnswerDtos.ToList();
            var allQuestionsInAnswerAreInTestToAnswer =
                answerDtos.All(ans => questionIds.Contains(ans.QuestionId));

            if (!allQuestionsInAnswerAreInTestToAnswer)
            {
                throw new TestAnswerException("One of the test answers consists of an invalid question id");
            }

            var fixedTestAnswers = new List<TestAnswerDto>();
            foreach (var question in testToAnswer.Questions)
            {
                var choicesIds = question.Choices.Select(c => c.Id);

                var questionAnswers =
                    answerDtos.Where(ans => ans.QuestionId == question.Id).ToList();

                var allChoicesInAnswerAreInQuestionToAnswer = questionAnswers.All(ans =>
                    ans.ChoiceId.HasValue && choicesIds.Contains(ans.ChoiceId.Value));

                if (!allChoicesInAnswerAreInQuestionToAnswer)
                {
                    const string msg = "One of the test answers consists of an invalid question id";
                    throw new TestAnswerException(msg);
                }

                var fixedQuestionAnswers =
                    QuestionAnswersFixAndValidate(question.QuestionType, questionAnswers);
                fixedTestAnswers.AddRange(fixedQuestionAnswers);
            }

            return fixedTestAnswers;
        }

        private static IEnumerable<TestAnswerDto> QuestionAnswersFixAndValidate(QuestionType questionType,
            IEnumerable<TestAnswerDto> testAnswers)
        {
            switch (questionType)
            {
                case QuestionType.Text:
                    return TextQuestionTypeAnswersFixAndValidate(testAnswers);

                case QuestionType.SingleChoice:
                    return SingleChoiceQuestionTypeAnswersFixAndValidate(testAnswers);

                case QuestionType.MultipleChoice:
                    return MultipleChoiceQuestionTypeAnswersFixAndValidate(testAnswers);
                
                default:
                    const string msg = "This type of question was not handled";
                    throw new ArgumentOutOfRangeException(nameof(questionType), msg);
            }
        }

        private static IEnumerable<TestAnswerDto> TextQuestionTypeAnswersFixAndValidate(
            IEnumerable<TestAnswerDto> testAnswersEnumerable)
        {
            var testAnswers = testAnswersEnumerable.ToList();
            var answersCount = testAnswers.Count;
            if (answersCount < 1)
            {
                var msg = $"{nameof(QuestionType.SingleChoice)} type question" +
                          $" has not been answered";
                throw new TestAnswerException(msg);
            }

            if (answersCount > 1)
            {
                var msg = $"{nameof(QuestionType.SingleChoice)} type question" +
                          $" has been answered more than once";
                throw new TestAnswerException(msg);
            }

            var hasNotChoiceIdMsg = $"{nameof(QuestionType.SingleChoice)} type question has not choice id";
            var singleAnswer = testAnswers.First();
            var newTestDto = new TestAnswerDto
            {
                Answer = null,
                ChoiceId = singleAnswer.ChoiceId ?? throw new TestAnswerException(hasNotChoiceIdMsg),
                QuestionId = singleAnswer.QuestionId
            };
            return new List<TestAnswerDto> {newTestDto};
        }

        private static IEnumerable<TestAnswerDto> SingleChoiceQuestionTypeAnswersFixAndValidate(
            IEnumerable<TestAnswerDto> testAnswersEnumerable)
        {
            var testAnswers = testAnswersEnumerable.ToList();
            var answersCount = testAnswers.Count;
            if (answersCount < 1)
            {
                var msg = $"{nameof(QuestionType.SingleChoice)} type question" +
                          $" has not been answered";
                throw new TestAnswerException(msg);
            }

            if (answersCount > 1)
            {
                var msg = $"{nameof(QuestionType.SingleChoice)} type question" +
                          $" has been answered more than once";
                throw new TestAnswerException(msg);
            }

            var hasNotChoiceIdMsg = $"{nameof(QuestionType.SingleChoice)} type question has not choice id";
            var singleAnswer = testAnswers.First();
            var newTestDto = new TestAnswerDto
            {
                Answer = null,
                ChoiceId = singleAnswer.ChoiceId ?? throw new TestAnswerException(hasNotChoiceIdMsg),
                QuestionId = singleAnswer.QuestionId
            };
            return new List<TestAnswerDto> {newTestDto};
        }

        private static IEnumerable<TestAnswerDto> MultipleChoiceQuestionTypeAnswersFixAndValidate(
            IEnumerable<TestAnswerDto> testAnswersEnumerable)
        {
            var distinctTestAnswers = testAnswersEnumerable
                .Where(ans => ans.ChoiceId is not null)
                .GroupBy(ans => ans.ChoiceId)
                .Select(group => group.First())
                .Select(ans => new TestAnswerDto
                {
                    Answer = null,
                    ChoiceId = ans.ChoiceId,
                    QuestionId = ans.QuestionId
                });
            
            return distinctTestAnswers.ToList();
        }
    }
}