using Microsoft.AspNetCore.Mvc;

namespace LoadBalancerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private static readonly string[] Backends = new[]
    {
        "http://localhost:5169/search",
        "http://localhost:5170/search"
    };

    private static int _counter = 0; // bruges til round-robin
    private readonly HttpClient _httpClient;

    public SearchController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string q, [FromQuery] int max = 10)
    {
        if (string.IsNullOrWhiteSpace(q))
            return BadRequest("Query cannot be empty");

        // Round Robin scheduling
        var backendIndex = Interlocked.Increment(ref _counter) % Backends.Length;
        var backendUrl = $"{Backends[backendIndex]}?q={Uri.EscapeDataString(q)}&max={max}";

        try
        {
            var response = await _httpClient.GetAsync(backendUrl);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error contacting backend: {ex.Message}");
        }
    }

    // Hjælpe-endpoint til at vise status
    [HttpGet("status")]
    public IActionResult Status()
    {
        return Ok(new
        {
            ActiveBackends = Backends,
            TotalRequests = _counter
        });
    }
}
