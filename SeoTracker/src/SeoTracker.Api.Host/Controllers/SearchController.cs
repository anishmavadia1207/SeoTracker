using Microsoft.AspNetCore.Mvc;

using SeoTracker.Abstractions.Services;
using SeoTracker.Api.Host.Mappers;
using SeoTracker.Api.Host.Models;

namespace SeoTracker.Api.Host.Controllers;

/// <summary>
/// Controller for search operations.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class SearchController(
    ISearchEngineManager manager,
    ILogger<SearchController> logger) : ControllerBase
{
    private readonly ISearchEngineManager _manager = manager;
    private readonly ILogger _logger = logger;



    /// <summary>
    /// Get the search rank of a given url based on a given search term.
    /// </summary>
    /// <param name="searchTerm">The term to use.</param>
    /// <param name="url">The url to use</param>
    /// <returns>A response containing the search rank.</returns>
    [HttpGet("rank")]
    public async Task<ActionResult<IEnumerable<SearchResult>>> GetRankAsync(
        [FromQuery] string searchTerm,
        [FromQuery] string url)
    {
        var results = await _manager.GetRanksAsync(searchTerm, url);

        var models = results.Select(r => r.MapToSearchResult());

        return Ok(models);
    }
}
