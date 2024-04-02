using FluentAssertions;

using Microsoft.Extensions.Logging;

using NSubstitute;

using SeoTracker.Core.Services.SearchEngines;

namespace SeoTracker.Core.Test.Unit.Services.SearchEngines;
public class GoogleSearchEngineServiceShould
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GoogleSearchEngineService> _logger;
    private readonly GoogleSearchEngineService _googleSearchEngineService;
    private readonly TestHttpMessageHandler _testHttpMessageHandler;
    private readonly HttpClient _httpClient;

    public GoogleSearchEngineServiceShould()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _logger = Substitute.For<ILogger<GoogleSearchEngineService>>();
        _testHttpMessageHandler = new TestHttpMessageHandler();
        _httpClient = new HttpClient(_testHttpMessageHandler)
        {
            BaseAddress = new Uri("http://google.com")
        };

        _httpClientFactory.CreateClient(GoogleSearchEngineService.Name).Returns(_httpClient);
        _googleSearchEngineService = new GoogleSearchEngineService(_httpClientFactory, _logger);
    }

    [Fact]
    public async Task Return_CorrectRank_WhenUrlIsFound()
    {
        var searchTerm = "example";
        var url = "http://example.com";
        var responseContent = "<a href=\"http://example.com\">";

        _testHttpMessageHandler.ResponseContent = responseContent;

        var searchRankDto = await _googleSearchEngineService.GetRankAsync(searchTerm, url, CancellationToken.None);

        searchRankDto.Rank.Should().Be(1);
    }

    [Fact]
    public async Task Return_NegativeRank_WhenUrlIsNotFound()
    {
        var searchTerm = "example";
        var url = "http://example.com";
        var responseContent = "<a href=\"http://anotherexample.com\">";

        _testHttpMessageHandler.ResponseContent = responseContent;

        var searchRankDto = await _googleSearchEngineService.GetRankAsync(searchTerm, url, CancellationToken.None);

        searchRankDto.Rank.Should().Be(-1);
    }

    [Fact]
    public async Task Throw_Exception_WhenRequestFails()
    {
        var searchTerm = "example";
        var url = "http://example.com";

        _testHttpMessageHandler.ExceptionToThrow = new HttpRequestException();
        _testHttpMessageHandler.ResponseContent = string.Empty;

        var action = async () => await _googleSearchEngineService.GetRankAsync(
            searchTerm,
            url,
            CancellationToken.None);

        await action.Should().ThrowAsync<HttpRequestException>();
    }
    private class TestHttpMessageHandler : HttpMessageHandler
    {
        public string? ResponseContent { get; set; }

        public Exception? ExceptionToThrow { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (ExceptionToThrow != null)
            {
                throw ExceptionToThrow;
            }

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(ResponseContent ?? string.Empty)
            };

            return Task.FromResult(response);
        }
    }

}