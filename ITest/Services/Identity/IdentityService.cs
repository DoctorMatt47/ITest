using System;
using System.Collections.Generic;
using System.Security.Claims;
using ITest.Data.Entities.Accounts;

namespace ITest.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        public ClaimsIdentity GetAccountIdentity(Account userAccount)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userAccount.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userAccount.Role.ToString()),
                new Claim(ClaimTypes.SerialNumber, userAccount.Id.ToString())
            };
            return new ClaimsIdentity(
                claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}