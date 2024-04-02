using SeoTracker.Abstractions.Models;
using SeoTracker.Abstractions.Services;

namespace SeoTracker.Core.Services.SearchEngines;

/// <summary>
/// The implementation of <see cref="ISearchEngineService"/> for Google.
/// </summary>
public class GoogleSearchEngineService : ISearchEngineService
{
    /// <inheritdoc/>
    public Task<SearchRankDto> GetRankAsync(
        string searchTerm,
        string url,
        CancellationToken cancellationToken) => throw new NotImplementedException();
}
