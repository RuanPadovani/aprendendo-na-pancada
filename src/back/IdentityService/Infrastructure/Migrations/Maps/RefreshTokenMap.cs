using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Migrations.Maps;

public sealed class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure (EntityTypeBuilder<RefreshToken> builder)
    {
        //Nome da Tabela.
        builder.ToTable("RefreshTokens");    

        // Chave Primaria.
        builder.HasKey(x => x.Id);

        //Validacao para campos requeridos.
        builder.Property(x => x.TokenHash).IsRequired();

        builder.HasOne<User>() //Refresh um User.
            .WithMany()       
            .HasForeignKey(x => x.UserId)       //User pode ter muitas cheaves.
            .OnDelete(DeleteBehavior.Cascade);  // se deletar User, deleta tokens.
    }
}
