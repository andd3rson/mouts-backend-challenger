using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("Sales-Item");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Quantity)
            .IsRequired();

        builder.Property(u => u.ProductId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.UnitPrice)
            .IsRequired();

        builder.Property(u => u.DiscountPercent)
            .IsRequired();

        builder.Property(u => u.TotalPrice).IsRequired();

       
    }
}
