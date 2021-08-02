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
        public async Task<IEnumerable<Test>> Get()
        {
            throw new NotImplementedException();
            //return await _db.Tests.ToListAsync();
        }

        [HttpGet()]
        public async Task<ActionResult<Test>> Get(Guid id)
        {
            //var test = await _testRepos.GetByIdAsync(id);
            //if (test is null)
            //{
            //    return NotFound();
            //}
            throw new NotImplementedException();
            //return Ok(test);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Test>> Post([FromBody] TestDto dto, CancellationToken cancellationToken)
        {
            if (User.Identity == null)
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

        [HttpPut]
        public async Task<ActionResult<Test>> Put([FromQuery] Guid id, [FromBody] TestDto dto,
            CancellationToken cancellationToken)
        {
            var updateTestCommand = new UpdateTestCommand
            {
                AccountId = Guid.Parse("F4138C2D-A800-444A-8B23-1E8F910BA607"),
                TestId = id,
                TestDto = dto
            };
            var updatedTest = await _mediator.Send(updateTestCommand, cancellationToken);
            return Ok(updatedTest);
        }

        [HttpDelete]
        public async Task<ActionResult<Test>> Delete(Test test)
        {
            throw new NotImplementedException();
        }
    }
}