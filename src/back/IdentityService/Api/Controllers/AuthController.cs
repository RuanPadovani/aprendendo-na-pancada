using Api.Contracts.Auth.Requests;
using Api.Contracts.Auth.Responses;
using Application.Interfaces;
using Application.UseCases.Auth;
using Application.UseCases.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    private readonly RefreshTokenUseCase _refreshTokenUseCase;

    public AuthController(
        ILogger<AuthController> logger,
        IAuthService authService, 
        RefreshTokenUseCase refreshTokenUseCase)
    {
        _logger = logger;
        _authService = authService;
        _refreshTokenUseCase  = refreshTokenUseCase;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password
        );

        var result = await _authService.LoginAsync(command);

        if (result is null)
            return Unauthorized("Email ou senha inválidos!");

        var response = new LoginResponse(
            result.AccessToken,
            result.RefreshToken,
            result.ExpiredInSeconds
        );

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await _refreshTokenUseCase.ExecuteAsync(command);
        return Ok(result);
    }
}