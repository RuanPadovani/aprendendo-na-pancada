using System.Text.Json.Serialization;

namespace IdentityService.Infrastructure.Integrations.IBGE;

internal sealed class IbgeCityDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("nome")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("microrregiao")]
    public IbgeMicrorregionResponse Microrregion { get; init; } = new();
}

internal sealed class IbgeMicrorregionResponse
{
    [JsonPropertyName("mesorregiao")]
    public IbgeMesorregionResponse Mesorregion{ get; init; } = new();
}

internal sealed class IbgeMesorregionResponse
{
    [JsonPropertyName("UF")]
    public IbgeUfResponse Uf { get; init; } = new();
}

internal sealed class IbgeUfResponse
{
    [JsonPropertyName("sigla")]
    public string Acronym { get; init; } = string.Empty;

    [JsonPropertyName("regiao")]
    public IbgeRegionResponse Region { get; init; } = new();
}

internal sealed class IbgeRegionResponse
{
    [JsonPropertyName("nome")]
    public string Name { get; init; } = string.Empty;
}
