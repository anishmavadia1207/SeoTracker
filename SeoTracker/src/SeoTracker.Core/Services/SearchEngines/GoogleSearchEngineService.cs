using System.Text.RegularExpressions;

using Microsoft.Extensions.Logging;

using SeoTracker.Abstractions.Models;
using SeoTracker.Abstractions.Services;

namespace SeoTracker.Core.Services.SearchEngines;

/// <summary>
/// The implementation of <see cref="ISearchEngineService"/> for Google.
/// </summary>
public class GoogleSearchEngineService(
    IHttpClientFactory httpClientFactory,
    ILogger<GoogleSearchEngineService> logger) : ISearchEngineService
{
    public const string Name = "Google";
    private readonly ILogger _logger = logger;
    private readonly HttpClient _client = httpClientFactory.CreateClient(Name);

    /// <inheritdoc/>
    public async Task<SearchRankDto> GetRankAsync(
        string searchTerm,
        string url,
        CancellationToken cancellationToken)
    {
        try
        {
            var start = 0;
            do
            {
                var pageResult = await GetPage(
                    searchTerm,
                    url,
                    start,
                    cancellationToken);

                if (pageResult is not null)
                {
                    return pageResult;
                }

                start += 10;
            } while (start < 90);

            return new SearchRankDto(Name, url, searchTerm, -1);
        }
        catch (Exception e)
        when (e is HttpRequestException || e is TaskCanceledException)
        {
            _logger.LogError(e, "Request failed due to an exception.");
            throw;
        }
        finally
        {
            _client?.Dispose();
        }
    }

    private async Task<SearchRankDto?> GetPage(
        string searchTerm,
        string url,
        int start,
        CancellationToken cancellationToken)
    {
        const string linkPattern = @"<a href=""(https?://[^\s""]+)""";
        var searchUrl = $"search?q={Uri.EscapeDataString(searchTerm)}&start={start}";
        var response = await _client.GetStringAsync(
            searchUrl,
            cancellationToken);

        var matches = Regex.Matches(response, linkPattern);
        var rank = start;
        foreach (Match match in matches)
        {
            var href = match.Groups[1].Value;
            if (href.Contains(url))
            {
                return new SearchRankDto(Name, url, searchTerm, rank);
            }
            rank++;
        }

        return null;
    }
}
