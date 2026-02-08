using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Context;

public class IdentityMigration: DbContext
{
    /// <summary>Trás as configurações de conexão com o banco.</summary>
    /// <param name="options">Carrega as configurações do banco.</param>
    public IdentityMigration(DbContextOptions<IdentityMigration> options) 
        : base (options){}


    /// <summary>Cria o mapeamento do banco, tabelas e colunas.</summary>
    /// <param name="modelBuilder">Esse método é chamado quando o EF está montando o “mapa” do banco.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityMigration).Assembly);
    }
  
}
