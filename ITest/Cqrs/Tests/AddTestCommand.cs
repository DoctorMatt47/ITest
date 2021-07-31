using System;
using ITest.Data.Entities.Accounts;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class AddTestCommand : IRequest<Test>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }
    }
}