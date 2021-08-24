using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ITest.Data.Entities.Accounts;

namespace ITest.Services.Identity
{
    public interface IIdentityService
    {
        ClaimsIdentity GetAccountIdentity(Account userAccount);
    }
}