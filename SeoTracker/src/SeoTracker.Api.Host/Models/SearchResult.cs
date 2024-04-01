namespace SeoTracker.Api.Host.Models;

/// <summary>
/// The result of a search.
/// </summary>
/// <param name="url"></param>
/// <param name="position"></param>
public class SearchResult(string url, int position)
{
    /// <summary>
    /// The URL which was searched for.
    /// </summary>
    public string Url { get; } = url;

    /// <summary>
    /// The position that was found.
    /// </summary>
    public int Position { get; } = position;
}
