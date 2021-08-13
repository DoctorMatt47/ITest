using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

            TestAnswersValidate(testToAnswer, command.TestAnswerDtos);

            var testAnswers =
                command.TestAnswerDtos.Select(dto => _mapper.Map<TestAnswer>(dto)).ToList();

            await _db.TestAnswers.AddRangeAsync(testAnswers, cancellationToken);
            return testAnswers;
        }

        private static void TestAnswersValidate(Test testToAnswer, IEnumerable<TestAnswerDto> testAnswerDtos)
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

                QuestionAnswersValidate(question.QuestionType, questionAnswers);
            }
        }

        private static void QuestionAnswersValidate(QuestionType questionType,
            ICollection<TestAnswerDto> testAnswers)
        {
            var answersCount = testAnswers.Count;
            switch (questionType)
            {
                case QuestionType.Text:
                {
                    if (answersCount < 1)
                    {
                        const string msg = "One of the test answers does not answer on text type question";
                        throw new TestAnswerException(msg);
                    }

                    if (answersCount > 1)
                    {
                        const string msg = "Text type question has more than one answer";
                        throw new TestAnswerException(msg);
                    }

                    if (testAnswers.First().Answer is null)
                    {
                        const string msg = "Text type question has not answer string";
                        throw new TestAnswerException(msg);
                    }

                    break;
                }
                case QuestionType.SingleChoice:
                {
                    if (answersCount != 1)
                    {
                        const string msg = "One of the test answers does not answer on single choice type question";
                        throw new TestAnswerException(msg);
                    }

                    if (testAnswers.First().Answer is not null)
                    {
                        const string msg = "Single choice type question has answer with not null answer string";
                        throw new TestAnswerException(msg);
                    }

                    break;
                }
                case QuestionType.MultipleChoice:
                {
                    if (testAnswers.First().Answer is not null)
                    {
                        const string msg = "Single choice type question has answer with not null answer string";
                        throw new TestAnswerException(msg);
                    }

                    break;
                }
                default:
                {
                    const string msg = "This type of question was not handled";
                    throw new ArgumentOutOfRangeException(nameof(questionType), msg);
                }
            }
        }
    }
}