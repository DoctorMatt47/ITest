using System;

namespace ITest.Data.Dtos.Requests.TestAnswers
{
    public class TestAnswerRequest
    {
        public string Answer { get; set; }
            
        public Guid? ChoiceId { get; set; }

        public Guid QuestionId { get; set; }
    }
}