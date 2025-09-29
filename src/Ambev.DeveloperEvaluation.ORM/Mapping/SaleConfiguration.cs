
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.SaleNumber)
              .UseIdentityAlwaysColumn();

        builder.HasIndex(u => u.SaleNumber);

        builder.Property(u => u.CustomerId)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.BranchId)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Items)
           .WithOne(x => x.Sale)
           .OnDelete(DeleteBehavior.Cascade);

    }
}
