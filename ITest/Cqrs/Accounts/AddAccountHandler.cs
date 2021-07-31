using System;
using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Accounts;
using ITest.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITest.Cqrs.Accounts
{
    public class AddAccountHandler : BaseHandler, IRequestHandler<AddAccountCommand, Account>
    {
        public AddAccountHandler(DatabaseContext db) : base(db)
        {
        }

        public async Task<Account> Handle(AddAccountCommand command, CancellationToken cancellationToken)
        {
            var accWithSameLoginOrMail = await _db.Accounts.FirstOrDefaultAsync(acc => 
                acc.Login == command.Login || acc.Mail == command.Mail,
                cancellationToken: cancellationToken);
            if (accWithSameLoginOrMail is not null)
            {
                if (accWithSameLoginOrMail.Login == command.Login)
                {
                    throw new AccountException("An account with this login already exists");
                }
                if (accWithSameLoginOrMail.Mail == command.Mail)
                {
                    throw new AccountException("An account with this mail already exists");
                }
            }
            var newAccount = new Account
            {
                Login = command.Login,
                Password = command.Password,
                Mail = command.Mail,
                City = command.City
            };
            await _db.Accounts.AddAsync(newAccount, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return newAccount;
        }
    }
}