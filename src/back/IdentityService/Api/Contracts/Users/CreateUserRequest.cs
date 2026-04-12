namespace IdentityService.Api.Contracts.Users;

public record CreateUserRequest(string Name, string Email, string Password);
