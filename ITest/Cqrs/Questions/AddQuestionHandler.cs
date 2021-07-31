using System.Threading;
using System.Threading.Tasks;
using ITest.Cqrs.Tests;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Questions
{
    public class AddQuestionHandler : BaseHandler, IRequestHandler<AddQuestionCommand, Question>
    {
        public AddQuestionHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Question> Handle(AddQuestionCommand command, CancellationToken cancellationToken)
        {
            var newQuestion = new Question
            {
                QuestionString = command.QuestionString,
                QuestionType = command.QuestionType,
                Test = command.Test
            };
            await _db.Questions.AddAsync(newQuestion, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return newQuestion;
        }
    }
}