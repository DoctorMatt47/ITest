using System;
using System.Collections.Generic;
using ITest.Data.Dtos.Requests.Tests;
using ITest.Data.Entities;
using ITest.Data.Entities.Tests;

namespace ITest.Data.Dtos.Responses.Tests
{
    public class TestQuestionsChoicesResponse : BaseResponse
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public uint VisitorsCount { get; set; }

        public List<QuestionChoicesRequest> Questions { get; set; }
            = new List<QuestionChoicesRequest>();
    }
    
    public class QuestionChoicesResponse : BaseResponse
    {
        public string QuestionString { get; set; }

        public QuestionType QuestionType { get; set; }

        public List<ChoiceRequest> Choices { get; set; } =
            new List<ChoiceRequest>();
    }
    
    public class ChoiceResponse : BaseResponse
    {
        public string ChoiceString { get; set; }
    }
    
}