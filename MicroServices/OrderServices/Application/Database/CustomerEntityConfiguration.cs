using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderServices.Application.Entities;

namespace OrderServices.Application.Database
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasIndex(b => b.Id)
            .IsUnique();
            builder.ToTable("Tbl_Customer");
            builder.HasKey(b => b.Id).HasName("Tbl_Customer_Pk");
            builder.Property(b => b.Id).HasColumnName("CustomerId");
            builder.Property(b => b.Name).HasColumnName("CustomerName");
            builder.Property(b => b.PhoneNumber).HasColumnName("PhoneNumber");
        }
    }
}