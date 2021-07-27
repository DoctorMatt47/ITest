using System.Collections.Generic;

namespace ITest.Data.Dtos.Tests
{
    public class TestDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }
}