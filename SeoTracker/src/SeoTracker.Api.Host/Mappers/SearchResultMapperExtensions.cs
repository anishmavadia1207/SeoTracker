using SeoTracker.Abstractions.Models;
using SeoTracker.Api.Host.Models;

namespace SeoTracker.Api.Host.Mappers;

/// <summary>
/// Mappers for the SearchResult.
/// </summary>
internal static class SearchResultMapperExtensions
{
    /// <summary>
    /// Map from a SearchRankDto to a SearchResult.
    /// </summary>
    /// <param name="this">The dto to map from.</param>
    /// <returns>The mapped result.</returns>
    public static SearchResult MapToSearchResult(this SearchRankDto @this) =>
        new(@this.EngineName, @this.Url, @this.Rank);
}
