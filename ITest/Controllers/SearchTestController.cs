using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITest.Cqrs.Tests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchTestController : Controller
    {
        private readonly IMediator _mediator;

        public SearchTestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{query.PagesToSkip}/{query.SearchString}")]
        public async Task<IEnumerable> GetBySearchString(GetTestsBySearchStringQuery query,
            CancellationToken cancellationToken)
        {
            var searchedTests = await _mediator.Send(query, cancellationToken);
            return searchedTests;
        }
    }
}