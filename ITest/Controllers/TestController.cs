using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITest.Cqrs.Accounts;
using ITest.Cqrs.Tests;
using Microsoft.AspNetCore.Mvc;
using ITest.Data.Dtos.Responses.Tests;
using ITest.Data.Entities.Tests;
using ITest.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/tests")]
    public class TestController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TestController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("{testId:guid}")]
        public async Task<ActionResult<TestResponse>> Get(Guid testId,
            CancellationToken cancellationToken)
        {
            var query = new GetTestByIdQuery(testId);
            var testToGet = await _mediator.Send(query, cancellationToken);
            if (testToGet is null)
            {
                return NotFound();
            }

            var testResponse = _mapper.Map<TestResponse>(testToGet);
            return Ok(testResponse);
        }

        [HttpDelete, Authorize]
        [Route("{testId:guid}")]
        public async Task<ActionResult> Delete(Guid testId,
            CancellationToken cancellationToken)
        {

            var deleteTestCommand = new DeleteTestCommand
            {
                TestId = testId,
                AccountId = User.GetUserAccountId()
            };
            await _mediator.Send(deleteTestCommand, cancellationToken);
            return NoContent();
        }
    }
}