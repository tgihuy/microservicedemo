﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using ProductService2.Application.Database;

#nullable disable

namespace ProductService2.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProductService2.Application.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ProductId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("AvailableQuantity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("ProductName");

                    b.Property<decimal>("Price")
                        .HasColumnType("DECIMAL(18, 2)")
                        .HasColumnName("Price");

                    b.HasKey("Id")
                        .HasName("Tbl_Product_Pk");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Tbl_Product", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
