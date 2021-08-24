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
        [Route("{searchString}/{pagesToSkip}")]
        public async Task<ActionResult<IEnumerable<Test>>> GetBySearchString(int pagesToSkip, string searchString,
            CancellationToken cancellationToken)
        {
            var getTestsBySearchStringQuery = new GetTestsBySearchStringQuery
            {
                PagesToSkip = pagesToSkip,
                SearchString = searchString
            };
            var searchedTests =
                await _mediator.Send(getTestsBySearchStringQuery, cancellationToken);
            return Ok(searchedTests);
        }

        [HttpGet]
        [Route("{query.SearchString}/pages-count")]
        public async Task<ActionResult<int>> GetPagesCount(string searchString,
            CancellationToken cancellationToken)
        {
            var getTestCountBySearchStringQuery = new GetTestCountBySearchStringQuery(searchString);
            var testCount = await _mediator.Send(getTestCountBySearchStringQuery, cancellationToken);
            var pagesCount = Math.Ceiling((double) testCount / MaxElementsOnOnePage);
            return Ok((int) pagesCount);
        }
    }
}