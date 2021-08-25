﻿using System.Threading;
using System.Threading.Tasks;
using ITest.Data;
using ITest.Data.Entities.Accounts;
using MediatR;

namespace ITest.Cqrs.Accounts
{
    public class GetAccountByLoginAndPasswordQuery : IRequest<Account>
    {
        public GetAccountByLoginAndPasswordQuery(string loginOrEmail, string password)
        {
            LoginOrEmail = loginOrEmail;
            Password = password;
        }

        public string LoginOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class GetAccountByLoginAndPasswordQueryHandler : BaseHandler,
        IRequestHandler<GetAccountByLoginAndPasswordQuery, Account>
    {
        private readonly IMediator _mediator;

        public GetAccountByLoginAndPasswordQueryHandler(DatabaseContext db, IMediator mediator) : base(db)
        {
            _mediator = mediator;
        }

        public async Task<Account> Handle(GetAccountByLoginAndPasswordQuery query,
            CancellationToken cancellationToken)
        {
            var userAccount = await _mediator.Send(
                new GetAccountByLoginQuery(query.LoginOrEmail),
                cancellationToken);
            return userAccount?.Password == query.Password ? userAccount : null;
        }
    }
}