using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITest.Attributes;
using ITest.Data.Entities.Tests;

namespace ITest.Data.RequestModels.Tests
{
    public class QuestionRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string QuestionString { get; set; }
        public QuestionType QuestionType { get; set; }

        [CollectionCount(0, 50)]
        public List<ChoiceRequest> Choices { get; set; } = new List<ChoiceRequest>();
    }
}