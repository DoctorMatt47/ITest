using ITest.Data.Entities.Accounts;

namespace ITest.Services.Tokens
{
    public interface ITokenService
    {
        string CreateJwtToken(Account account);
    }
}