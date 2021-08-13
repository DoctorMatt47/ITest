using System;
using System.ComponentModel.DataAnnotations;

namespace ITest.Data.Dtos.TestAnswers
{
    public class TestAnswerDto
    {
        public string Answer { get; set; }
            
        public Guid? ChoiceId { get; set; }

        public Guid QuestionId { get; set; }
    }
}