using System;

namespace ITest.Data.Dtos.Responses
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}