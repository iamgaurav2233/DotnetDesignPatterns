using CarSearchApi.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSearchApi.Mvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuggestController : ControllerBase
{
    private readonly ElasticsearchService _service;
    private readonly ILogger<SuggestController> _logger;

    public SuggestController(ElasticsearchService service, ILogger<SuggestController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetSuggestions([FromQuery] string q, [FromQuery] string? region)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Query parameter 'q' cannot be empty.");
        }

        try
        {
            var results = await _service.GetSuggestionsAsync(q, region ?? "in");
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching suggestions for query: {Query}", q);
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}