using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITest.Attributes;
using ITest.Models.Accounts;

namespace ITest.Models.Tests
{
    public class Test : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength=5)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public uint VisitorsCount { get; set; } = 0;
        
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        [CollectionCount(1, 200)]
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();
    }
}
