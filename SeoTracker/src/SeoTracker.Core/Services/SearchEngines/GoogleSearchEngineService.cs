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
        var searchUrl = $"search?num=100&q={Uri.EscapeDataString(searchTerm)}";

        try
        {
            var response = await _client.GetStringAsync(
                searchUrl,
                cancellationToken);

            var linkPattern = @"<a href=""(https?://[^\s""]+)""";

            var matches = Regex.Matches(response, linkPattern);

            var rank = 1;
            foreach (Match match in matches)
            {
                var href = match.Groups[1].Value;
                if (href.Contains(url))
                {
                    return new SearchRankDto(Name, url, searchTerm, rank);
                }
                rank++;
            }

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
}
