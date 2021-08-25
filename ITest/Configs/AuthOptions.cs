using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ITest.Configs
{
    public static class AuthOptions
    {
        public const string Issuer = "ITestServer";
        public const string Audience = "ITestClient";
        public const string Key = "71574d2b0054c63f2ffa8319c8ec9e10a69be721e39ee55a6d2c52ab5e93ff45";
        public const int Lifetime = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOptions.Key));
        }
    }
}