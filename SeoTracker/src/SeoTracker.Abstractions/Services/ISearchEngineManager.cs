using SeoTracker.Abstractions.Models;

namespace SeoTracker.Abstractions.Services;

/// <summary>
/// Defines the operations for querying all engines.
/// </summary>
public interface ISearchEngineManager
{
    /// <summary>
    /// Query all search engines registered to find the ranking of a url.
    /// </summary>
    /// <param name="searchTerm">The search term</param>
    /// <param name="url">The url</param>
    /// <returns>The results from all of the registered search engines.</returns>
    Task<IEnumerable<SearchRankDto>> GetRanksAsync(string searchTerm, string url);
}
