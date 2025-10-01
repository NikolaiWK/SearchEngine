using ConsoleSearch;
using Microsoft.AspNetCore.Mvc;
namespace MyProject.API;


    [ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly SearchLogic _searchLogic;

    public SearchController()
    {
        _searchLogic = new SearchLogic(new DatabaseSqlite());
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string q, [FromQuery] int max = 10)
    {
        if (string.IsNullOrWhiteSpace(q))
            return BadRequest("Query cannot be empty");

        var query = q.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
        var results = _searchLogic.Search(query, max);
        return Ok(results);
    }
}

