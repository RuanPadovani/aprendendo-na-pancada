using System.Data.Common;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infrastructure.Repositories.Query;

public class UserRepository : IUserRepository
{
    private readonly string _conStr = null!;
    /// <summary>Injeção de Dependencias</summary>
    public UserRepository(IConfiguration config)
    {
        _conStr = config.GetConnectionString("DefaultConnection")
            ??  throw new KeyNotFoundException($"The connection string parameter was not found for MariaDB in application settings.");
    }

    public async Task<IEnumerable<User>> ListAllUser()
    {
        using var conn = new MySqlConnection(_conStr);

        const string query = @"
                            select 
                                UserId, Name, Email, CreateAt, IsActive
                            from 
                                Users;
                            ";

        using var cmd = new MySqlCommand(query, conn);
        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();
        
        var listAllUsers = new List<User>();
        var mapper = new UserMapper();

        while (await reader.ReadAsync())
        {
            listAllUsers.Add(mapper.Map(reader));
        }
    
        return listAllUsers;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        using var conn = new MySqlConnection(_conStr);

        const string query = @"
                            select 
                                UserId, Name, Email, CreateAt, IsActive
                            from 
                                Users
                            where UserId = @Id
                            limit 1;
                            ";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", id);
        
        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();

        if(!await reader.ReadAsync())
            return null;

        var mapper = new UserMapper();

        return mapper.Map(reader);
    }

    public async Task<bool> CreateUser(string name, string email)
    {
        using var conn = new MySqlConnection(_conStr);

        const string query = @"
                            insert into Users
                                    (UserId, Name, Email, CreateAt, IsActive)
                            values (@Id, @Name, @Email, @CreateAt, IsActive);
        ";

        using var cmd = new MySqlCommand(query,conn);
        cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
        cmd.Parameters.AddWithValue("@Name", name);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@CreateAt", DateTime.UtcNow);
        cmd.Parameters.AddWithValue("@IsActive", 1);

        await conn.OpenAsync();

        var result = await cmd.ExecuteNonQueryAsync();
        
        return result > 0 ? true : false;
    }

    public async Task<bool> EditUser(Guid id, string name, string email)
    {
        using var conn = new MySqlConnection(_conStr);

        const string query =@"
                            update Users
                            set
                                Name = @Name
                                Email = @Email
                            where UserId = @Id;
        ";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("Name", name);
        cmd.Parameters.AddWithValue("Email", email);

        await conn.OpenAsync();

        var affected = await cmd.ExecuteNonQueryAsync();

        return affected > 0;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        using var conn = new MySqlConnection(_conStr);
        
        const string query =@"
                            delete from Users
                            where UserId = @Id;
        ";

        using var cmd = new MySqlCommand(query,conn);

        cmd.Parameters.AddWithValue("Id", id);

        await conn.OpenAsync();

        var affected = await cmd.ExecuteNonQueryAsync();

        return affected > 0;
    }

    private sealed class UserMapper
    {
        private int _userId = -1, _name = -1, _email = -1, _createAt = -1, _isActive = -1;
        private bool _init ;
        public User Map(DbDataReader dr)
        {
            if (!_init)
            {
                _userId = dr.GetOrdinal("UserId");
                _name = dr.GetOrdinal("Name");
                _email = dr.GetOrdinal("Email");
                _createAt = dr.GetOrdinal("CreateAt");
                _isActive = dr.GetOrdinal("IsActive");
            }

            var userId = dr.GetGuid(_userId);
            var name = dr.GetString(_name);
            var email = dr.GetString(_email);
            var createAt = dr.GetDateTime(_createAt);
            var isActice = dr.GetBoolean(_isActive);

            return new User(userId, name, email,createAt, isActice);
        }
    }

}
