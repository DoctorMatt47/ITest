using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITest.Attributes;

namespace ITest.Data.RequestModels.Tests
{
    public class TestRequest
    {
        [Required]
        [StringLength(100, MinimumLength=2)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(300, MinimumLength=5)]
        public string Description { get; set; }

        [CollectionCount(1, 200)]
        public List<QuestionRequest> Questions { get; set; } = new List<QuestionRequest>();
    }
}