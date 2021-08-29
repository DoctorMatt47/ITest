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

        public List<QuestionChoicesResponse> Questions { get; set; }
            = new List<QuestionChoicesResponse>();
    }
    
    public class QuestionChoicesResponse : BaseResponse
    {
        public string QuestionString { get; set; }

        public QuestionType QuestionType { get; set; }

        public List<ChoiceResponse> Choices { get; set; } =
            new List<ChoiceResponse>();
    }
    
    public class ChoiceResponse : BaseResponse
    {
        public string ChoiceString { get; set; }
    }
    
}