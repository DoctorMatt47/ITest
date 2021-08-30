using System;

namespace ITest.Data.Dtos.Responses.TestAnswers
{
    public class TestAnswerResponse : BaseResponse
    {
        public string Answer { get; set; }
            
        public Guid? ChoiceId { get; set; }

        public Guid QuestionId { get; set; }
    }
}