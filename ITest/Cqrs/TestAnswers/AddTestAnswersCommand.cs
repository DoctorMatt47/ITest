using System;
using System.Collections.Generic;
using ITest.Data.Dtos.TestAnswers;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.TestAnswers
{
    public class AddTestAnswersCommand : IRequest<IEnumerable<TestAnswer>>
    {
        public IEnumerable<TestAnswerDto> TestAnswerDtos { get; set; }
        
        public Guid AccountId { get; set; }
                
        public Guid TestId { get; set; }
    }
}