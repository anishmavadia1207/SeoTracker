namespace SeoTracker.Api.Host.Models;

public class SearchResult(string url, int position)
{
    public string Url { get; } = url;
    public int Position { get; } = position;
}
