using BasketServices.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Application.Database
{
    public class CustomerBasketEntityConfiguration : IEntityTypeConfiguration<CustomerBasket>
    {
        public void Configure(EntityTypeBuilder<CustomerBasket> builder)
        {
            builder.ToTable("Tbl_CustomerBasket");
            builder.HasKey(c => c.CustomerId).HasName("Tbl_CustomerBasket_Id");
            builder.Property(c => c.CustomerId).HasColumnName("CustomerId");

            var navigation = builder.Metadata.FindNavigation(nameof(CustomerBasket.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}