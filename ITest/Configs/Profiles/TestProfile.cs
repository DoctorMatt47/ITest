using AutoMapper;
using ITest.Cqrs.Choices;
using ITest.Cqrs.Questions;
using ITest.Cqrs.Tests;
using ITest.Data.Dtos.Tests;
using ITest.Data.Entities.Tests;

namespace ITest.Configs.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile() {
            CreateMap<TestDto, AddTestCommand>();
            CreateMap<QuestionDto, AddQuestionCommand>();
            CreateMap<ChoiceDto, AddChoiceCommand>();
            CreateMap<TestDto, AddTestQuestionsChoicesCommand>();
            CreateMap<TestDto, Test>();
            CreateMap<QuestionDto, Question>();
            CreateMap<ChoiceDto, Choice>();
            CreateMap<Test, TestDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<Choice, ChoiceDto>();
        }
    }
}