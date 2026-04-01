using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly string _conStr;
    public RefreshTokenRepository(IConfiguration config)
    {
        _conStr = config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
    {
        using var conn = new MySqlConnection(_conStr);

        const string query = @"
            SELECT Id, UserId, TokenHash, ExpiresAt, Revoked, CreatedAt
            FROM RefreshTokens
            WHERE TokenHash = @TokenHash
            LIMIT 1;";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@TokenHash", tokenHash);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;

        return MapRefreshToken(reader);
    }

    public async Task SaveAsync(RefreshToken token)
    {
        using var conn = new MySqlConnection(_conStr);

        const string query = @"
            INSERT INTO RefreshTokens (Id, UserId, TokenHash, ExpiresAt, Revoked, CreatedAt)
            VALUES (@Id, @UserId, @TokenHash, @ExpiresAt, @Revoked, @CreatedAt);";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Id", token.Id);
        cmd.Parameters.AddWithValue("@UserId", token.UserId);
        cmd.Parameters.AddWithValue("@TokenHash", token.TokenHash);
        cmd.Parameters.AddWithValue("@ExpiresAt", token.ExpiresAt);
        cmd.Parameters.AddWithValue("@Revoked", token.Revoked);
        cmd.Parameters.AddWithValue("@CreatedAt", token.CreatedAt);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task RevokeAsync(RefreshToken token)
    {
        using var conn = new MySqlConnection(_conStr);

        const string query = @"
            UPDATE RefreshTokens
            SET Revoked = TRUE
            WHERE Id = @Id;";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Id", token.Id);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    private static RefreshToken MapRefreshToken(MySqlDataReader reader)
    {
        // Reflection para setar propriedades privadas — alternativa: construtor interno
        var id = reader.GetGuid("Id");
        var userId = reader.GetGuid("UserId");
        var tokenHash = reader.GetString("TokenHash");
        var expiresAt = reader.GetDateTime("ExpiresAt");
        var revoked = reader.GetBoolean("Revoked");
        var createdAt = reader.GetDateTime("CreatedAt");

        // Como as props são private set, use um construtor interno no Domain
        return RefreshToken.Reconstitute(id, userId, tokenHash, expiresAt, revoked, createdAt);
    }
}
