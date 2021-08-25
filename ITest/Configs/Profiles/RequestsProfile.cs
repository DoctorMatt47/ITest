using AutoMapper;
using ITest.Cqrs.Accounts;
using ITest.Cqrs.Tests;
using ITest.Data.Dtos.Requests.Accounts;
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
            CreateMap<DeleteAccountRequest, DeleteAccountCommand>();
            CreateMap<DeleteAccountRequest, DeleteAccountCommand>();
        }
    }
}