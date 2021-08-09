using AutoMapper;
using ITest.Cqrs.Choices;
using ITest.Data.Dtos.Tests;
using ITest.Data.Entities.Tests;

namespace ITest.Configs.Profiles
{
    public class ChoiceProfile : Profile
    {
        public ChoiceProfile()
        {
            CreateMap<ChoiceDto, Choice>();
            CreateMap<ChoiceDto, AddChoiceCommand>();
            CreateMap<Choice, ChoiceDto>();
        }
    }
}