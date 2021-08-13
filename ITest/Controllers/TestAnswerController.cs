using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ITest.Cqrs.TestAnswers;
using ITest.Data.Dtos.TestAnswers;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class TestAnswerController : Controller
    {
        private readonly IMediator _mediator;

        public TestAnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TestAnswer>> GetByAccountId(GetTestAnswersByAccountIdQuery query,
            CancellationToken cancellationToken)
        {
            var testAnswers = await _mediator.Send(query, cancellationToken);
            return testAnswers;
        }

        [HttpPost, Authorize]
        [Route("{testId}")]
        public async Task<IEnumerable<TestAnswer>> Post(Guid testId, [FromBody] IEnumerable<TestAnswerDto> dtos,
            CancellationToken cancellationToken)
        {
            var addAnswersCommand = new AddTestAnswersCommand
            {
                TestId = testId,
                TestAnswerDtos = dtos,
                AccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.SerialNumber))
            };
            return await _mediator.Send(addAnswersCommand, cancellationToken);
        }
    }
}