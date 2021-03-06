using System.Collections;
using AutoMapper;
using ITest.Cqrs.Accounts;
using ITest.Cqrs.TestAnswers;
using ITest.Cqrs.Tests;
using ITest.Data.Dtos.Requests.Accounts;
using ITest.Data.Dtos.Requests.TestAnswers;
using ITest.Data.Dtos.Requests.Tests;

namespace ITest.Configs.Profiles
{
    public class RequestsProfile : Profile
    {
        public RequestsProfile()
        {
            CreateMap<LoginAccountRequest, GetAccountByLoginAndPasswordQuery>();
            CreateMap<RegisterAccountRequest, AddAccountCommand>();
            CreateMap<DeleteAccountRequest, DeleteAccountCommand>();
            
            CreateMap<TestQuestionsChoicesRequest, AddTestQuestionsChoicesCommand>();
            CreateMap<QuestionChoicesRequest, AddTestQuestionsChoicesCommand.QuestionChoicesDto>();
            CreateMap<ChoiceRequest, AddTestQuestionsChoicesCommand.QuestionChoicesDto.ChoiceDto>();
            
            CreateMap<DeleteAccountRequest, DeleteAccountCommand>();
            CreateMap<DeleteAccountRequest, DeleteAccountCommand>();
            
            CreateMap<TestAnswersRequest, AddTestAnswersByTestIdCommand>();
            CreateMap<TestAnswerRequest, AddTestAnswersByTestQuestionsChoicesCommand.TestAnswerDto>();
        }
    }
}