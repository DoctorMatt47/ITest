using System;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Choices
{
    public class AddChoiceCommand : IRequest<Choice>
    {
        public string ChoiceString { get; set; }
        
        public Question Question { get; set; }
    }
}