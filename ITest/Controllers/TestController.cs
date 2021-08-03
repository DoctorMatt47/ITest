using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITest.Cqrs.Accounts;
using ITest.Cqrs.Choices;
using ITest.Cqrs.Questions;
using ITest.Cqrs.Tests;
using Microsoft.AspNetCore.Mvc;
using ITest.Data;
using ITest.Data.Dtos.Tests;
using ITest.Data.Entities.Tests;
using ITest.Exceptions.Tests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/tests")]
    public class TestController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<TestDto> _testValidator;
        private readonly IMapper _mapper;

        public TestController(
            IMediator mediator,
            IValidator<TestDto> testValidator,
            IMapper mapper)
        {
            _mediator = mediator;
            _testValidator = testValidator;
            _mapper = mapper;
        }
        

        [HttpGet]
        public async Task<ActionResult<Test>> Get(GetTestByIdQuery query, CancellationToken cancellationToken)
        {
            var testToGet = await _mediator.Send(query, cancellationToken);
            if (testToGet is null)
                return NotFound();
            return Ok(testToGet);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Test>> Post([FromBody] TestDto dto, CancellationToken cancellationToken)
        {
            if (User.Identity is null)
            {
                return Unauthorized();
            }

            var validationResult = await _testValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(new {errors = validationResult.Errors});
            }

            var userAccount =
                await _mediator.Send(new GetAccountByLoginQuery(User.Identity.Name), cancellationToken);

            var addCommand = new AddTestQuestionsChoicesCommand
            {
                TestDto = dto,
                AccountId = userAccount.Id
            };
            var createdTest = await _mediator.Send(addCommand, cancellationToken);
            var createdTestDto = _mapper.Map<TestDto>(createdTest);

            const string domain = "localhost:5001";
            var uriString = $"https://{domain}/test/{createdTest.Id}";
            return Created(new Uri(uriString), new {createdTest.Id, createdTestDto});
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<Test>> Put([FromQuery] Guid id, [FromBody] TestDto dto,
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
                TestId = id,
                TestDto = dto
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
        
        [HttpDelete, Authorize]
        public async Task<ActionResult<Test>> Delete(Guid testId, CancellationToken cancellationToken)
        {
            if (User.Identity is null)
            {
                return Unauthorized();
            }
            
            var userAccount =
                await _mediator.Send(new GetAccountByLoginQuery(User.Identity.Name), cancellationToken);
            
            var deleteTestCommand = new DeleteTestCommand(testId, userAccount.Id);
            await _mediator.Send(deleteTestCommand, cancellationToken);
            return NoContent();
        }
    }
}