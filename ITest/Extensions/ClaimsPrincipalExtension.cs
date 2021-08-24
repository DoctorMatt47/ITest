using System;
using System.Security.Claims;

namespace ITest.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserAccountId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.SerialNumber)?.Value;
            if (!Guid.TryParse(userId, out var userGuid))
            {
                throw new ArgumentException(nameof(ClaimTypes.SerialNumber));
            }

            return userGuid;
        }
    }
}