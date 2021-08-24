using System.Collections.Generic;
using ITest.Data.Entities.Tests;

namespace ITest.Data.Dtos.Requests.Tests
{
    public class TestQuestionsChoicesRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<QuestionChoicesRequest> Questions { get; set; }
            = new List<QuestionChoicesRequest>();
    }

    public class QuestionChoicesRequest
    {
        public string QuestionString { get; set; }

        public QuestionType QuestionType { get; set; }

        public List<ChoiceRequest> Choices { get; set; } =
            new List<ChoiceRequest>();
    }

    public class ChoiceRequest
    {
        public string ChoiceString { get; set; }
    }
}