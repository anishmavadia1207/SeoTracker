namespace SeoTracker.Abstractions.Services;

/// <summary>
/// Interface for search engine service.
/// </summary>
public interface ISearchEngineService
{
    /// <summary>
    /// Get the rank of a given url using a search term.
    /// </summary>
    /// <param name="searchTerm">The term to use to determine the rank.</param>
    /// <param name="url">The URL to get the rank for.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The rank of the URL.</returns>
    Task<int> GetRankAsync(
        string searchTerm,
        string url,
        CancellationToken cancellationToken);
}
