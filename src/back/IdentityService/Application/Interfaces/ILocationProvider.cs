namespace IdentityService.Application.Interfaces;

public interface ILocationProvider
{
    /// <summary>Metodo para buscar dados da populacao com base em cidades.</summary>
    /// <param name="uf">Codigo da Cidade.</param>
    /// <param name="city">Cidade.</param>
    /// <param name="ct">Codigo para cancelamento.</param>
    Task<CityResult?> ResolveCityAsync(string uf, string city, CancellationToken ct);
}
