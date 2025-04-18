using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

internal class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(_ => _.SaleNumber).IsRequired();
        builder.Property(_ => _.TotalSaleAmount).IsRequired();
        builder.Property(_ => _.StoreName).IsRequired().HasMaxLength(100);

        builder.Property(u => u.PurchaseStatus)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(_ => _.SoldAt).IsRequired().HasColumnName("SaleAt");
        builder.Property(_ => _.CreatedAt).IsRequired();
        builder.Property(_ => _.UpdatedAt).IsRequired(false);
        builder.Property(_ => _.CancelledAt).IsRequired(false);
        builder.Property(_ => _.DeletedAt).IsRequired(false);

        builder.HasOneAsShadow(_ => _.CreatedBy);
        builder.HasOneAsShadow(_ => _.BoughtBy);
        builder.HasOneAsShadow(_ => _.CancelledBy, required: false);
        builder.HasOneAsShadow(_ => _.DeletedBy, required: false);

        builder.HasMany(_ => _.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(_ => _.SaleNumber)
            .IsUnique();

        builder.Ignore(_ => _.ActiveItems);
    }
}
