using ITest.Configs;
using ITest.Data;
using ITest.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _db;

        public AccountController(DatabaseContext db) => _db = db;

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var person = await _db.Accounts.FirstOrDefaultAsync(x =>
                x.Login == username && x.Password == password);
            if (person == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login)
            };
            var claimsIdentity = new
                ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginAsync(Account user)
        {
            var identity = await GetIdentityAsync(user.Login, user.Password);
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

        [HttpPost("/register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Account user)
        {
            if (user is null)
            {
                return BadRequest(new {errorText = "User is null"});
            }

            var account = await _db.Accounts.FirstOrDefaultAsync(acc
                => acc.Login == user.Login || acc.Mail == user.Mail);
            if (account.Login == user.Login)
            {
                return BadRequest(new {errorText = "An account with this login already exists"});
            }

            if (account.Mail == user.Mail)
            {
                return BadRequest(new {errorText = "An account with this mail already exists"});
            }

            var newAccount = new Account
            {
                Id = new Guid(),
                Login = user.Login,
                Password = user.Password,
                Mail = user.Mail,
                City = user.City,
            };
            await _db.Accounts.AddAsync(newAccount);
            await _db.SaveChangesAsync();
            return Ok(newAccount);
        }
    }
}