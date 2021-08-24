using ITest.Data.Entities;

namespace ITest.Data.Dtos.Responses.Tests
{
    public class TestResponse : BaseResponse
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public uint VisitorsCount { get; set; }
    }
}