using System.Net;

using FluentAssertions;

using SeoTracker.Test.Integration.Configuration;

namespace SeoTracker.Test.Integration.Controllers;

[Collection(nameof(SharedTestCollection))]
public class SearchControllerShould(SeoTrackerWebApplicationFactory factory)
{
    private readonly HttpClient _client = factory.Client;

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("searchTerm=test", null)]
    [InlineData("searchTerm=test", "")]
    [InlineData(null, "url=test.com")]
    [InlineData("", "url=test.com")]
    public async Task Return_BadRequest_When_RequestParametersAreNotSupplied(
        string? searchTermQuery,
        string? urlSearchQuery)
    {
        var response = await _client.GetAsync($"/search/position?{searchTermQuery}&{urlSearchQuery}");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
