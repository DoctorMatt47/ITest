using AutoMapper;
using ITest.Cqrs.Questions;
using ITest.Data.Dtos.Tests;
using ITest.Data.Entities.Tests;

namespace ITest.Configs.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();
            CreateMap<QuestionDto, AddQuestionCommand>();
        }
    }
}