using System.Net.Http.Json;

namespace MyBlazorApp.Services;

public class SearchService
{
    private readonly HttpClient _http;

    public SearchService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<string>> SearchAsync(string query, int max = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<string>();

        var url = $"search?q={Uri.EscapeDataString(query)}&max={max}";
        return await _http.GetFromJsonAsync<List<string>>(url) ?? new List<string>();
    }
}
