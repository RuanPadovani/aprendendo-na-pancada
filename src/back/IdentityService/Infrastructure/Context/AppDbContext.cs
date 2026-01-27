using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options) 
        : base (options){}

    public DbSet<User> Users => Set<User>();

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //User
        modelBuilder.Entity<User>().
            Property(c => c.Name)
                .HasMaxLength(100) 
                .IsRequired();


        modelBuilder.Entity<User>() 
            .Property(c => c.Email)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<User>().
            Property(c => c.UserId)
            .IsFixedLength()
            .IsRequired();
    }
}
