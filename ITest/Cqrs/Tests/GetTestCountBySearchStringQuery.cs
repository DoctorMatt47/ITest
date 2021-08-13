using MediatR;

namespace ITest.Cqrs.Tests
{
    public class GetTestCountBySearchStringQuery : IRequest<int>
    {
        public GetTestCountBySearchStringQuery(string searchString) => SearchString = searchString;
        
        public string SearchString { get; set; }
    }
}