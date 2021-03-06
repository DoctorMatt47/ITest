using AutoMapper;
using ITest.Cqrs.Accounts;
using ITest.Data.Dtos.Requests.Accounts;
using ITest.Data.Dtos.Responses.TestAnswers;
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
            CreateMap<Question, QuestionChoicesResponse>();
            CreateMap<Choice, ChoiceResponse>();

            CreateMap<TestAnswer, TestAnswerResponse>();
        }
    }
}