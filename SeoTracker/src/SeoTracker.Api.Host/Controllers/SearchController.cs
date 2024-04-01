using Microsoft.AspNetCore.Mvc;

using SeoTracker.Api.Host.Models;

namespace SeoTracker.Api.Host.Controllers;

/// <summary>
/// Controller for search operations.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class SearchController : ControllerBase
{

    [HttpGet("position")]
    public async Task<ActionResult<SearchResult>> GetPositionAsync(
        [FromQuery] string searchTerm,
        [FromQuery] string url)
    {
        await Task.CompletedTask;
        return Ok(new SearchResult(url, -1));
    }
}
