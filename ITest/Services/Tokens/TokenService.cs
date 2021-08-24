using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ITest.Data.Entities.Accounts;
using ITest.Services.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ITest.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IIdentityService _identity;

        private const string Issuer = "ITestServer";
        private const string Audience = "ITestClient";
        private const string Key = "71574d2b0054c63f2ffa8319c8ec9e10a69be721e39ee55a6d2c52ab5e93ff45";
        private const int Lifetime = 1; // время жизни токена - 1 минута

        public TokenService(IIdentityService identity) => _identity = identity;

        public string CreateJwtToken(Account account)
        {
            var identity = _identity.GetAccountIdentity(account);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(Issuer, Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(Lifetime)),
                signingCredentials: new SigningCredentials(
                    GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}