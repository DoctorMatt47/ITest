using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITest.Cqrs.TestAnswers;
using ITest.Data.Dtos.Requests.TestAnswers;
using ITest.Data.Entities.Tests;
using ITest.Exceptions.Cqrs;
using ITest.Extensions;
using ITest.Services.Tokens;
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
        private readonly IMapper _mapper;

        public TestAnswerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TestAnswer>> GetByAccountId(GetTestAnswersByAccountIdQuery query,
            CancellationToken cancellationToken)
        {
            var testAnswers = await _mediator.Send(query, cancellationToken);
            return testAnswers;
        }

        [HttpPost, Authorize]
        [Route("{testId:guid}")]
        public async Task<ActionResult<IEnumerable<TestAnswer>>> Post(Guid testId, 
            [FromBody] TestAnswersRequest request,
            CancellationToken cancellationToken)
        {
            var addTestAnswersByTestIdCommand = 
                _mapper.Map<AddTestAnswersByTestIdCommand>(request);
            addTestAnswersByTestIdCommand.TestId = testId;
            addTestAnswersByTestIdCommand.AccountId = User.GetUserAccountId();

            IEnumerable<TestAnswer> answers;
            try
            {
                answers = await _mediator.Send(addTestAnswersByTestIdCommand, cancellationToken);
            }
            catch (CqrsValidationException e)
            {
                return BadRequest(new { message = e.Message, errors = e.Data });
            }

            return Created("", new{answers});
        }
    }
}