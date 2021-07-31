using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Choices
{
    public class AddChoiceHandler : BaseHandler, IRequestHandler<AddChoiceCommand, Choice>
    {
        public AddChoiceHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Choice> Handle(AddChoiceCommand command, CancellationToken cancellationToken)
        {
            var newChoice = new Choice
            {
                ChoiceString = command.ChoiceString,
                Question = command.Question,
            };
            await _db.Choices.AddAsync(newChoice, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return newChoice;
        }
    }
}