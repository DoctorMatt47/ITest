using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITest.Data.Entities.Accounts;
using ITest.Models.Tests;

namespace ITest.Data.Entities.Tests
{
    public class Test : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public uint VisitorsCount { get; set; } = 0;
        
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();
    }
}
