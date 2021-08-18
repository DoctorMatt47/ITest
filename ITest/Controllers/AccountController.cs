using ITest.Configs;
using ITest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ITest.Cqrs.Accounts;
using ITest.Data.Entities.Accounts;
using ITest.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator) => _mediator = mediator;

        private async Task<ClaimsIdentity> GetIdentityAsync(GetAccountByLoginAndPasswordQuery query,
            CancellationToken cancellationToken)
        {
            var userAccount = await _mediator.Send(query, cancellationToken);
            if (userAccount == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userAccount.Login),
                new Claim(ClaimTypes.SerialNumber, userAccount.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userAccount.Role.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] GetAccountByLoginAndPasswordQuery query,
            CancellationToken cancellationToken)
        {
            var identity = await GetIdentityAsync(query, cancellationToken);
            if (identity == null)
            {
                return BadRequest(new {message = "Invalid username or password."});
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials
                (
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                jwtToken = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AddAccountCommand command,
            CancellationToken cancellationToken)
        {
            Account newAccount;
            try
            {
                newAccount = await _mediator.Send(command, cancellationToken);
            }
            catch (AccountException e)
            {
                return BadRequest(new {errorText = e.Message});
            }
            const string domain = "localhost:5001";
            var uriString = $"https://{domain}/test/{newAccount.Id}";
            return Created(new Uri(uriString), newAccount);
        }
        
        [HttpDelete, Authorize]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] string password,
            CancellationToken cancellationToken)
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.SerialNumber);
            if (accountIdClaim is null)
            {
                return Unauthorized();
            }

            var command = new DeleteAccountCommand
            {
                AccountId = Guid.Parse(accountIdClaim.Value),
                Password = password
            };
            try
            {
               await _mediator.Send(command, cancellationToken);
            }
            catch (AccountException e)
            {
                return BadRequest(new {errorText = e.Message});
            }
            return NoContent();
        }
    }
}