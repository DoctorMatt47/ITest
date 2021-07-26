using System.ComponentModel.DataAnnotations;

namespace ITest.Data.RequestModels.Tests
{
    public class ChoiceRequest
    {
        [StringLength(100, MinimumLength=1)]
        public string ChoiceString { get; set; }
    }
}