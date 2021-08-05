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
                return BadRequest(new {errorText = "Invalid username or password."});
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
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
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
    }
}