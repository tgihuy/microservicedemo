using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderServices.Application.Entities;

namespace OrderServices.Application.Database
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasIndex(b => b.Id)
            .IsUnique();
            builder.ToTable("Tbl_OrderItem");
            builder.HasKey(b => b.Id).HasName("Tbl_OrderItem_Pk");
            builder.Property(b => b.Id).HasColumnName("Id");
            builder.Property(b => b.ProductName).HasColumnName("ProductName");
            builder.Property(b => b.ProductId).HasColumnName("ProductId");
            builder.Property(b => b.Quantity).HasColumnName("Quantity");
        }
    }
}