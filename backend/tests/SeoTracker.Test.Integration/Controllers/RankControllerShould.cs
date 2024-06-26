﻿using System.Net;
using System.Net.Http.Json;

using Bogus;

using FluentAssertions;
using FluentAssertions.Execution;

using SeoTracker.Api.Host.Models;
using SeoTracker.Test.Integration.Configuration;

namespace SeoTracker.Test.Integration.Controllers;

[Collection(nameof(SharedTestCollection))]
public class RankControllerShould(SeoTrackerWebApplicationFactory factory)
{
    private readonly HttpClient _client = factory.Client;
    private const string RankSearchUrl = "/api/rank";


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
        var response = await _client.GetAsync($"{RankSearchUrl}?{searchTermQuery}&{urlSearchQuery}");

        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Return_Positions_When_RequestParametersAreSupplied()
    {
        var faker = new Faker();
        var url = faker.Internet.Url();
        var searchTerm = faker.Lorem.Word();

        var response = await _client.GetAsync($"{RankSearchUrl}?searchTerm={searchTerm}&url={url}");
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<SearchResult>>();

        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().NotBeNull();
        content?.All(c => c.Url == url).Should().BeTrue();
    }
}
