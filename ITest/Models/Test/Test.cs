using System;
using System.Collections.Generic;

namespace ITest.Models.Test
{
    public class Test : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int VisitorsCount { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();
        public List<UserTestAnswer> TestResponses { get; set; } = new List<UserTestAnswer>();
    }
}
