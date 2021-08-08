using System.Collections.Generic;
using ITest.Data.Entities.Tests;
using MediatR;

namespace ITest.Cqrs.Tests
{
    public class GetTestsBySearchStringQuery : IRequest<ICollection<Test>>
    {
        public GetTestsBySearchStringQuery(string searchString) => SearchString = searchString;
        
        public string SearchString { get; set; }
        public int PagesToSkip { get; set; }
    }
}