using FluentAssertions;
using FluentAssertions.Execution;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NSubstitute;

using SeoTracker.Abstractions.Models;
using SeoTracker.Abstractions.Services;
using SeoTracker.Api.Host.Controllers;
using SeoTracker.Api.Host.Models;
using SeoTracker.Test.Generators;

namespace SeoTracker.Api.Host.Test.Unit.Controllers;
public class RankControllerShould
{
    private readonly ISearchEngineManager _manager;
    private readonly ILogger<RankController> _logger;
    private readonly RankController _controller;

    public RankControllerShould()
    {
        _manager = Substitute.For<ISearchEngineManager>();
        _logger = Substitute.For<ILogger<RankController>>();
        _controller = new RankController(_manager, _logger);
    }

    [Fact]
    public async Task Get_Ranks_ReturnsOkResult_When_ResultsAreAvailable()
    {
        // Arrange
        var searchTerm = "example";
        var url = "http://example.com";

        var searchResults = new List<SearchRankDto>
        {
            SearchRankDtoGenerator.Generate()
        };

        _manager.GetRanksAsync(searchTerm, url).Returns(searchResults);


        var actionResult = await _controller.GetRankAsync(searchTerm, url);

        using var scope = new AssertionScope();
        actionResult.Result.Should().BeOfType<OkObjectResult>();

        var okObjectResult = (OkObjectResult)actionResult.Result!;
        okObjectResult.Value.Should().NotBeNull();

        var searchResult = ((IEnumerable<SearchResult>)okObjectResult.Value!).ToArray()[0];
        searchResult.SearchEngineName.Should().Be(searchResults[0].SearchEngineName);
        searchResult.Url.Should().Be(searchResults[0].Url);
        searchResult.Query.Should().Be(searchResults[0].Query);
        searchResult.Rank.Should().Be(searchResults[0].Rank);
    }
}
