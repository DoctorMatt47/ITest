using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ITest.Configs;
using ITest.Data.Entities.Accounts;
using ITest.Services.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace ITest.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IIdentityService _identity;

        public TokenService(IIdentityService identity) => _identity = identity;

        public string CreateJwtToken(Account account)
        {
            var identity = _identity.GetAccountIdentity(account);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}