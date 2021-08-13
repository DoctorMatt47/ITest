using AutoMapper;
using ITest.Data.Dtos.TestAnswers;
using ITest.Data.Entities.Tests;

namespace ITest.Configs.Profiles
{
    public class TestAnswerProfile : Profile
    {
        public TestAnswerProfile()
        {
            CreateMap<TestAnswerDto, TestAnswer>();
        }
        
    }
}