namespace SeoTracker.Api.Host.Models;

/// <summary>
/// The result of a search.
/// </summary>
/// <param name="url">The URL which was searched for.</param>
/// <param name="rank">The rank that was found.</param>
/// <param name="searchEngineName">The name of the search engine.</param>
public class SearchResult(
    string searchEngineName,
    string url,
    int rank)
{
    /// <summary>
    /// The name of the search engine.
    /// </summary>
    public string SearchEngineName { get; } = searchEngineName;

    /// <summary>
    /// The URL which was searched for.
    /// </summary>
    public string Url { get; } = url;

    /// <summary>
    /// The rank that was found.
    /// </summary>
    public int Rank { get; } = rank;
}
