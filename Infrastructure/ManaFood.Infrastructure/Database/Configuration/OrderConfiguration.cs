using ManaFood.Domain.Entities;
using ManaFood.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Aplica configurações base
        builder.ConfigureBaseEntity();

        // Configurações específicas
        builder.ToTable("Orders");
        builder.Property(e => e.OrderConfirmationTime)
            .HasColumnName("order_confirmation_time")
            .IsRequired(false);
        builder.Property(e => e.OrderStatus)
            .HasColumnName("order_status")
            .IsRequired();
        builder.Property(e => e.TotalAmount)
            .HasColumnName("total_amount")
            .IsRequired();
        builder.Property(e => e.PaymentMethod)
            .HasColumnName("payment_method")
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(o => o.Products)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}