using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService2.Application.Entities;

namespace ProductService2.Application.Database
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(b => b.Id)
           .IsUnique();
            builder.ToTable("Tbl_Product");
            builder.HasKey(b => b.Id).HasName("Tbl_Product_Pk");
            builder.Property(b => b.Id).HasColumnName("ProductId");
            builder.Property(b => b.Name).HasColumnName("ProductName");
            builder.Property(b => b.Price).HasColumnName("Price");
            builder.Property(b => b.AvailableQuantity).HasColumnName("AvailableQuantity");
        }
    }
}