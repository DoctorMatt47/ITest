using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ITest.Attributes;

namespace ITest.Models.Tests
{
    public class Test : BaseEntity
    {
        [Required]
        [NotNull]
        [StringLength(100, MinimumLength=5)]
        public string Title { get; set; }
        
        [Required]
        [NotNull]
        [StringLength(300, MinimumLength=5)]
        public string Description { get; set; }

        public uint VisitorsCount { get; set; } = 0;
        
        [CollectionCount(1, 200)]
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<UserTestAnswer> TestAnswers { get; set; } = new List<UserTestAnswer>();
    }
}
