using ManaFood.Domain.Entities;
using ManaFood.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Aplica configurações base
        builder.ConfigureBaseEntity();

        // Configurações específicas
        builder.ToTable("Users");
        builder.Property(e => e.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Cpf)
            .HasColumnName("cpf")
            .IsRequired()
            .HasMaxLength(11);
        builder.Property(e => e.Password)
            .HasColumnName("password")
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(e => e.Birthday)
            .HasColumnName("birthday")
            .IsRequired()
            .HasColumnType("date");
        builder.Property(e => e.UserType)
            .HasColumnName("user_type")
            .IsRequired();
    }
}