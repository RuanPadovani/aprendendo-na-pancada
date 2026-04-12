namespace IdentityService.Application.Auth.DTOs;

public sealed record LoginResponse(string AccessToken, string RefreshToken, int ExpiredInSeconds);
