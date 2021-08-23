﻿using System;
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
        [Route("preview/{testId}")]
        public async Task<ActionResult<Test>> GetPreview(string testId,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(testId, out var testIdGuid))
            {
                return BadRequest("Test id is incorrect");
            }

            var query = new GetTestByIdQuery(testIdGuid);
            var testToGet = await _mediator.Send(query, cancellationToken);
            if (testToGet is null)
            {
                return NotFound();
            }

            return Ok(testToGet);
        }
        
        [HttpGet]
        [Route("{testId}")]
        public async Task<ActionResult<Test>> Get(string testId,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(testId, out var testIdGuid))
            {
                return BadRequest("Test id is incorrect");
            }

            var query = new GetTestQuestionsChoicesByTestIdQuery(testIdGuid);
            var testToGet = await _mediator.Send(query, cancellationToken);
            if (testToGet is null)
            {
                return NotFound();
            }

            var testToGetDto = _mapper.Map<TestDto>(testToGet);
            return Ok(testToGetDto);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult> Post([FromBody] TestDto dto,
            CancellationToken cancellationToken)
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
            return Created(new Uri(uriString), createdTest.Id);
        }

        [HttpPut, Authorize]
        [Route("{testId}")]
        public async Task<ActionResult<Test>> Put(Guid testId, [FromBody] TestDto dto,
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
        [Route("{testId}")]
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