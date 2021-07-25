using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ITest.Attributes;
using ITest.Models.Accounts;

namespace ITest.Models.Tests
{
    public class Test : BaseEntity
    {
        [Required]
        [NotNull]
        [StringLength(100, MinimumLength=5)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [NotNull]
        [StringLength(300, MinimumLength=5)]
        public string Description { get; set; } = string.Empty;

        public uint VisitorsCount { get; set; } = 0;
        
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = default!;

        [CollectionCount(1, 200)]
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<TestAnswer> TestAnswers { get; set; } = new List<TestAnswer>();
    }
}
