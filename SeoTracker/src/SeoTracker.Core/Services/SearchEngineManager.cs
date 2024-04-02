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
    public Task<IEnumerable<SearchRankDto>> GetRanksAsync(
        string searchTerm,
        string url) => throw new NotImplementedException();
}
