using System.Collections.Generic;

namespace ITest.Data.Dtos.Requests.TestAnswers
{
    public class TestAnswersRequest
    {
        public IEnumerable<TestAnswerRequest> Answers { get; set; }
    }
}