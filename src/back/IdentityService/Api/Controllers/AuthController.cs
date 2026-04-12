using IdentityService.Api.Contracts.Auth.Requests;
using IdentityService.Application.Auth.Commands.Login;
using IdentityService.Application.Auth.Commands.Logout;
using IdentityService.Application.Auth.Commands.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _sender.Send(command, ct);

        if (!result.IsSuccess)
            return Unauthorized(result.Error);

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken ct)
    {
        var command = new RefreshTokenCommand(request.RefreshToken);
        var result = await _sender.Send(command, ct);

        if (!result.IsSuccess)
            return Unauthorized(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request, CancellationToken ct)
    {
        var command = new LogoutCommand(request.RefreshToken);
        var result = await _sender.Send(command, ct);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }
}
