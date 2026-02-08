using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infrastructure.Repositories.Query;

public class User
{
    private readonly string _conStr = null!;
    /// <summary>Injeção de Dependencias</summary>
    public User(IConfiguration config)
    {
        var conString = config.GetConnectionString(_conStr);
        if (string.IsNullOrEmpty(conString))
            throw new KeyNotFoundException($"The connection string parameter was not found for MariaDB in application settings.");

        _conStr = conString;
    }
    
    /*
    public async Task<IEnumerable<User>> ListAllUser()
    {
        using var conn = new MySqlConnection(_conStr);

        const string query =@"
                            select UserId, Name, Email, CreateAt, IsActive
                            from Users;";

        using var cmd = new MySqlCommand(query, conn);
        await conn.OpenAsync();
        

        return
    }
    
    private User Reader(DbDataReader dr)
    {
        return new User
        {
            Convert.ToInt64(dr["UserId"]),
            
        };
        
    }
    */
}
