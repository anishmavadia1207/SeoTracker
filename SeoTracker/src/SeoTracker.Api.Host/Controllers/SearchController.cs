using Microsoft.AspNetCore.Mvc;

using SeoTracker.Abstractions.Services;
using SeoTracker.Api.Host.Models;

namespace SeoTracker.Api.Host.Controllers;

/// <summary>
/// Controller for search operations.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class SearchController(
    ISearchEngineService searchEngineService,
    ILogger<SearchController> logger) : ControllerBase
{
    private readonly ISearchEngineService _searchEngineService = searchEngineService;
    private readonly ILogger _logger = logger;



    /// <summary>
    /// Get the search rank of a given url based on a given search term.
    /// </summary>
    /// <param name="searchTerm">The term to use.</param>
    /// <param name="url">The url to use</param>
    /// <returns>A response containing the search rank.</returns>
    [HttpGet("rank")]
    public async Task<ActionResult<SearchResult>> GetRankAsync(
        [FromQuery] string searchTerm,
        [FromQuery] string url)
    {
        var result = await _searchEngineService.GetRankAsync(
            searchTerm,
            url,
            CancellationToken.None);

        return Ok(new SearchResult(url, result));
    }
}
