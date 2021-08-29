using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITest.Cqrs.Accounts;
using ITest.Data.Dtos.Requests.Accounts;
using ITest.Data.Dtos.Responses.Accounts;
using ITest.Data.Entities.Accounts;
using ITest.Exceptions.Cqrs;
using ITest.Extensions;
using ITest.Services.Tokens;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ITokenService _token;

        public AccountController(
            IMediator mediator,
            ITokenService token,
            IMapper mapper)
        {
            _token = token;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginAccountRequest>> Login([FromBody] LoginAccountRequest request,
            CancellationToken cancellationToken)
        {
            var getAccountQuery = _mapper.Map<GetAccountByLoginAndPasswordQuery>(request);
            Account accountToLogin;
            try
            {
                accountToLogin = await _mediator.Send(getAccountQuery, cancellationToken);
            }
            catch (CqrsValidationException e)
            {
                return BadRequest(new {message = e.Message, errors = e.Data});
            }
            catch (Exception e)
            {
                return BadRequest(new {message = e.Message, errors = e.Data});
            }

            if (accountToLogin is null)
            {
                return BadRequest(new {message = "Incorrect username or password."});
            }

            return Ok(new LoginAccountResponse
            {
                JwtToken = _token.CreateJwtToken(accountToLogin),
                AccountId = accountToLogin.Id.ToString()
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<Guid>> Register([FromBody] RegisterAccountRequest request,
            CancellationToken cancellationToken)
        {
            var addAccountCommand = _mapper.Map<AddAccountCommand>(request);
            Account newAccount;
            try
            {
                newAccount = await _mediator.Send(addAccountCommand, cancellationToken);
            }
            catch (CqrsValidationException e)
            {
                return BadRequest(new {message = e.Message, errors = e.Data});
            }
            
            return Created($"profile/{newAccount.Id}", new{newAccount.Id});
        }

        [HttpDelete, Authorize]
        [Route("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteAccountRequest request,
            CancellationToken cancellationToken)
        {
            var deleteAccountCommand = _mapper.Map<DeleteAccountCommand>(request);
            deleteAccountCommand.AccountId = User.GetUserAccountId();
            try
            {
                await _mediator.Send(deleteAccountCommand, cancellationToken);
            }
            catch (CqrsValidationException e)
            {
                return BadRequest(new {message = e.Message, errors = e.Data});
            }

            return NoContent();
        }
    }
}