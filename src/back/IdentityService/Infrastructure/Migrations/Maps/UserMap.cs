using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Migrations.Context.Maps;

public sealed class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure (EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.UserId);
        
        // Requisitos minimos.
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();

        // Define como chave unica.
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Role)
            .IsRequired()
            .HasDefaultValue(Role.User);
    }
}
