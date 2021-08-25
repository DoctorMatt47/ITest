namespace ITest.Configs
{
    public static class RegularExpression
    {
        public const string Login = "^[a-zA-Z0-9_-]*$";
        public const string Password = "^(?=.*[a-zA-Z])(?!.*\\s).*$";
        public const string Email = "^[-\\w.]+@([A-z0-9][-A-z0-9]+\\.)+[A-z]{2,4}$";
        public const string City = "^[a-zA-Z]+(?:[\\s-][a-zA-Z]+)*$";
        
        public const string TestTitle = "^[ a-zA-Z0-9_!?-]*$";
        public const string TestDescription = "^[ a-zA-Z0-9_!?-]*$";
        public const string TestQuestionString = "^[ a-zA-Z0-9_!?-]*$";
        public const string TestChoiceString = "^[ a-zA-Z0-9_!?-]*$";
    }
}