using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderServices.Application.Entities;

namespace OrderServices.Application.Database
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasIndex(b => b.Id)
            .IsUnique();
            builder.ToTable("Tbl_Order");
            builder.HasKey(b => b.Id).HasName("Tbl_Order_Pk");
            builder.Property(b => b.Id).HasColumnName("OrderId");
            builder.Property(b => b.OrderDate).HasColumnName("OrderDate");
            builder.Property(b => b.CustomerId).HasColumnName("CustomerId");
            builder.Property(b => b.Street).HasColumnName("Street");
            builder.Property(b => b.City).HasColumnName("City");
            builder.Property(b => b.District).HasColumnName("District");
            builder.Property(b => b.AdditionalAdress).HasColumnName("AdditionalAddress");
            builder.HasOne<Customer>()
            .WithMany()
            .IsRequired(false)
            .HasForeignKey(e => e.CustomerId);


            var navigation = builder.Metadata.FindNavigation(nameof(Order.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}