using FluentAssertions;

using Microsoft.Extensions.Logging;

using NSubstitute;

using SeoTracker.Abstractions.Services;
using SeoTracker.Core.Services;
using SeoTracker.Test.Generators;

namespace SeoTracker.Core.Test.Unit.Services;
public class SearchEngineManagerShould
{
    private readonly SearchEngineManager _searchEngineManager;
    private readonly ISearchEngineService _searchEngineService1;
    private readonly ISearchEngineService _searchEngineService2;
    public SearchEngineManagerShould()
    {
        _searchEngineService1 = Substitute.For<ISearchEngineService>();
        _searchEngineService2 = Substitute.For<ISearchEngineService>();

        _searchEngineService1.GetRankAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(async _ =>
            {
                await Task.Delay(100);
                return SearchRankDtoGenerator.Generate();
            });

        _searchEngineService2.GetRankAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(async _ =>
            {
                await Task.Delay(100);
                return SearchRankDtoGenerator.Generate();
            });

        _searchEngineManager = new SearchEngineManager(
            [_searchEngineService1, _searchEngineService2],
            Substitute.For<ILogger<SearchEngineManager>>());
    }

    [Fact]
    public async Task Call_EngineServices_When_GetRankIsCalled()
    {
        var term = "example";
        var url = "http://example.com";
        var token = CancellationToken.None;

        _ = await _searchEngineManager.GetRanksAsync(term, url, token);

        _ = _searchEngineService1.Received(1).GetRankAsync(term, url, token);
        _ = _searchEngineService2.Received(1).GetRankAsync(term, url, token);
    }

    [Fact]
    public async Task Return_ResultsFromAllEngineServices_When_GetRankIsCalled()
    {
        var term = "example";
        var url = "http://example.com";
        var token = CancellationToken.None;

        var results = await _searchEngineManager.GetRanksAsync(term, url, token);

        results.Should().HaveCount(2);
    }
}