namespace ITest.Data.Dtos.Requests.Accounts
{
    public class RegisterAccountRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}