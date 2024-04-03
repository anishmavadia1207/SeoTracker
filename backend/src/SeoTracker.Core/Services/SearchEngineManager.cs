using Microsoft.Extensions.Logging;

using SeoTracker.Abstractions.Models;
using SeoTracker.Abstractions.Services;

namespace SeoTracker.Core.Services;

/// <summary>
/// The implementation of <see cref="ISearchEngineManager"/>
/// </summary>
/// <param name="searchEngines">The search engines</param>
/// <param name="logger">The logger</param>
public class SearchEngineManager(
    IEnumerable<ISearchEngineService> searchEngines,
    ILogger<SearchEngineManager> logger) : ISearchEngineManager
{
    private readonly IEnumerable<ISearchEngineService> _searchEngineServices = searchEngines;
    private readonly ILogger _logger = logger;

    ///<inheritdoc/>
    public async Task<IEnumerable<SearchRankDto>> GetRanksAsync(
        string searchTerm,
        string url,
        CancellationToken cancellationToken = default)
    {
        var tasks = _searchEngineServices.Select(async s =>
        {
            _logger.LogDebug(
                "Getting rank for {SearchTerm} from {SearchEngine}",
                searchTerm,
                s.GetType().Name);

            return await s.GetRankAsync(searchTerm, url, cancellationToken);
        });

        var results = await Task.WhenAll(tasks);

        return results;
    }
}
