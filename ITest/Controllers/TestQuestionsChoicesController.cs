using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITest.Cqrs.Accounts;
using ITest.Cqrs.Tests;
using ITest.Data.Dtos.Requests.Tests;
using ITest.Data.Dtos.Responses.Tests;
using ITest.Data.Entities.Tests;
using ITest.Exceptions.Cqrs;
using ITest.Exceptions.Tests;
using ITest.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/tests-questions-choices")]
    public class TestQuestionsChoicesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TestQuestionsChoicesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{testId}")]
        public async Task<ActionResult<TestQuestionsChoicesResponse>> Get(string testId,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(testId, out var testIdGuid))
            {
                return BadRequest(new {message = "Test id is incorrect"});
            }

            var query = new GetTestQuestionsChoicesByTestIdQuery(testIdGuid);
            var testQuestionsChoicesToGet = await _mediator.Send(query, cancellationToken);
            if (testQuestionsChoicesToGet is null)
            {
                return NotFound();
            }

            var testQuestionsChoicesResponse =
                _mapper.Map<TestQuestionsChoicesResponse>(testQuestionsChoicesToGet);
            return Ok(testQuestionsChoicesResponse);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Guid>> Create([FromBody] TestQuestionsChoicesRequest request,
            CancellationToken cancellationToken)
        {
            var addTestQuestionsChoicesCommand = _mapper.Map<AddTestQuestionsChoicesCommand>(request);
            addTestQuestionsChoicesCommand.AccountId = User.GetUserAccountId();

            Test createdTest;
            try
            {
                createdTest = await _mediator.Send(addTestQuestionsChoicesCommand, cancellationToken);
            }
            catch (CqrsValidationException e)
            {
                return BadRequest(new {message = e.Message, errors = e.Data});
            }
            catch (CqrsNotFoundException e)
            {
                return NotFound();
            }

            var uriString = $"/test-preview/{createdTest.Id}";
            return Created(uriString, createdTest.Id);
        }

        [HttpPut, Authorize]
        [Route("{testId}")]
        public async Task<ActionResult<Test>> Update(Guid testId,
            [FromBody] TestQuestionsChoicesRequest request,
            CancellationToken cancellationToken)
        {
            if (User.Identity is null)
            {
                return Unauthorized();
            }

            var userAccount =
                await _mediator.Send(new GetAccountByLoginQuery(User.Identity.Name), cancellationToken);

            var updateTestCommand = new UpdateTestCommand
            {
                AccountId = userAccount.Id,
                TestId = testId,
                TestRequest = request
            };

            Test updatedTest;
            try
            {
                updatedTest = await _mediator.Send(updateTestCommand, cancellationToken);
            }
            catch (TestForbiddenException e)
            {
                return Forbid(e.Message);
            }
            catch (TestNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (TestException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(updatedTest);
        }
    }
}