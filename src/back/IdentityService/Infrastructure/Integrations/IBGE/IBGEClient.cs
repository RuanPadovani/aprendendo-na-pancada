using System.Net;
using System.Text.Json;

namespace IdentityService.Infrastructure.Integrations.IBGE;

internal sealed class IBGEClient
{
    private readonly HttpClient _http;
    public IBGEClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<IbgeCityDto>> ListCitiesByUfAsync(string uf, CancellationToken ct)
    {
        using var response = await _http.GetAsync($"/api/v1/localidades/estados/{uf}/municipios", ct);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return Array.Empty<IbgeCityDto>();

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(ct);
        var data = await JsonSerializer.DeserializeAsync<List<IbgeCityDto>>(stream, JsonOptions, ct);

        return data ?? Array.Empty<IbgeCityDto>();
    }   
}
