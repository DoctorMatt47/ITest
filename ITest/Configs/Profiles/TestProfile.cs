using AutoMapper;
using ITest.Cqrs.Choices;
using ITest.Cqrs.Questions;
using ITest.Cqrs.Tests;
using ITest.Data.Dtos.Tests;

namespace ITest.Configs.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile() {
            CreateMap<TestDto, AddTestCommand>();
            CreateMap<QuestionDto, AddQuestionCommand>();
            CreateMap<ChoiceDto, AddChoiceCommand>();
        }
    }
}