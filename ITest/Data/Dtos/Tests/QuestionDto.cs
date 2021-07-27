using System.Collections.Generic;
using ITest.Data.Entities.Tests;

namespace ITest.Data.Dtos.Tests
{
    public class QuestionDto
    {
        public string QuestionString { get; set; }
        
        public QuestionType QuestionType { get; set; }
        
        public List<ChoiceDto> Choices { get; set; } = new List<ChoiceDto>();
    }
}