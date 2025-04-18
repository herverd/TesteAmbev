using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(_ => _.Quantity).IsRequired();
        builder.Property(_ => _.UnitPrice).IsRequired().HasPrecision(10, 2);
        builder.Property(_ => _.DiscountPercentage).IsRequired().HasPrecision(5, 2);
        builder.Property(_ => _.DiscountAmount).IsRequired().HasPrecision(10, 2);
        builder.Property(_ => _.TotalPreDiscounts).IsRequired().HasPrecision(14, 2);
        builder.Property(_ => _.TotalAmount).IsRequired().HasPrecision(14, 2);

        builder.Property(u => u.PurchaseStatus)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(_ => _.CreatedAt).IsRequired();
        builder.Property(_ => _.UpdatedAt).IsRequired(false);
        builder.Property(_ => _.CancelledAt).IsRequired(false);
        builder.Property(_ => _.DeletedAt).IsRequired(false);

        builder.HasOneAsShadow(_ => _.CreatedBy);
        builder.HasOneAsShadow(_ => _.CancelledBy, required: false);
        builder.HasOneAsShadow(_ => _.DeletedBy, required: false);

        builder.HasOneAsShadow(_ => _.Product, deleteBehavior: DeleteBehavior.SetNull);
    }
}
