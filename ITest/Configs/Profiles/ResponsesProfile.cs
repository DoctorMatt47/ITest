using AutoMapper;
using ITest.Cqrs.Accounts;
using ITest.Data.Dtos.Requests.Accounts;
using ITest.Data.Dtos.Responses.Tests;
using ITest.Data.Entities.Tests;

namespace ITest.Configs.Profiles
{
    public class ResponsesProfile : Profile
    {
        public ResponsesProfile()
        {
            CreateMap<Test, TestResponse>();
            CreateMap<Test, TestQuestionsChoicesResponse>();
        }
    }
}