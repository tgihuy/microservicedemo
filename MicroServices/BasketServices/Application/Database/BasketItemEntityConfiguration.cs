using Microservices.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Application.Database
{
    public class BasketItemEntityConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasIndex(b => b.Id)
            .IsUnique();
            builder.ToTable("Tbl_BasketItem");
            builder.HasKey(b => b.Id).HasName("Tbl_BasketItem_Pk");
            builder.Property(b => b.Id).HasColumnName("BasketItemId");
            builder.Property(b => b.ProductId).HasColumnName("ProductId");
            builder.Property(b => b.ProductName).HasColumnName("ProductName");
            builder.Property(b => b.Quantity).HasColumnName("Quantity");
            builder.Property(b => b.Status).HasColumnName("Status");
        }
    }
}