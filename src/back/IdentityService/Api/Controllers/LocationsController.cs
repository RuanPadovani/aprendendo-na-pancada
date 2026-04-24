using Application.Common.Mediator;
using IdentityService.Application.Location.Queries.ResolveCity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{uf}/{city}")]
    public async Task<IActionResult> GetCity(string uf, string city, CancellationToken ct)
    {
        var result = await _mediator.Send(new ResolveCityQuery(uf, city), ct);

        if (result is null)
            return NotFound("Município não encontrado.");

        return Ok(result);
    }
}
