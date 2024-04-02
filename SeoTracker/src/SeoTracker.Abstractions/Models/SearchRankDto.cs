namespace SeoTracker.Abstractions.Models;

/// <summary>
/// The DTO containing information about a search ranking.
/// </summary>
public class SearchRankDto(
    string engineName,
    string url,
    string query,
    int rank)
{
    /// <summary>
    /// The name of the search engine.
    /// </summary>
    public string EngineName { get; } = engineName;

    /// <summary>
    /// The URL of the page searched for.
    /// </summary>
    public string Url { get; } = url;

    /// <summary>
    /// The query that was searched for.
    /// </summary>
    public string Query { get; } = query;

    /// <summary>
    /// The rank of the page.
    /// </summary>
    public int Rank { get; } = rank;
}
