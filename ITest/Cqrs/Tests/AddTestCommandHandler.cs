using System;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Tests
{
    public class AddTestCommandHandler : BaseHandler, IRequestHandler<AddTestCommand, Test>
    {
        public AddTestCommandHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Test> Handle(AddTestCommand command, CancellationToken cancellationToken)
        {
            var newTest = new Test
            {
                Title = command.Title,
                Description = command.Description,
                Account = command.Account
            };
            await _db.Tests.AddAsync(newTest, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return newTest;
        }
    }
}