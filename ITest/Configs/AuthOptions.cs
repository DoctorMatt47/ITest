using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ITest.Configs
{
    public class AuthOptions
    {
        public const string ISSUER = "ITestServer"; // издатель токена
        public const string AUDIENCE = "ITestClient"; // потребитель токена
        const string KEY = "71574d2b0054c63f2ffa8319c8ec9e10a69be721e39ee55a6d2c52ab5e93ff45";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
