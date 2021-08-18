using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITest.Cqrs.Tests;
using ITest.Data.Entities.Tests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITest.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchTestController : Controller
    {
        private const int MaxElementsOnOnePage = 20;
        private readonly IMediator _mediator;

        public SearchTestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{query.PagesToSkip}/{query.SearchString}")]
        public async Task<IEnumerable<Test>> GetBySearchString(GetTestsBySearchStringQuery query,
            CancellationToken cancellationToken)
        {
            var searchedTests = await _mediator.Send(query, cancellationToken);
            return searchedTests;
        }

        [HttpGet]
        [Route("pages-count/{query.SearchString}")]
        public async Task<int> GetPagesCount(GetTestCountBySearchStringQuery query,
            CancellationToken cancellationToken)
        {
            var testCount = await _mediator.Send(query, cancellationToken);
            var a = (double) testCount / MaxElementsOnOnePage;
            return (int) Math.Ceiling(a);
        }
    }
}