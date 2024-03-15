﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using OrderServices.Application.Database;

#nullable disable

namespace OrderServices.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OrderServices.Application.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("CustomerId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CustomerName");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("Id")
                        .HasName("Tbl_Customer_Pk");

                    b.ToTable("Tbl_Customer", (string)null);
                });

            modelBuilder.Entity("OrderServices.Application.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("OrderId");

                    b.Property<string>("AdditionalAdress")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("AdditionalAddress");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("City");

                    b.Property<string>("CustomerId")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("CustomerId");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("District");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("OrderDate");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("Street");

                    b.HasKey("Id")
                        .HasName("Tbl_Order_Pk");

                    b.HasIndex("CustomerId");

                    b.ToTable("Tbl_Order", (string)null);
                });

            modelBuilder.Entity("OrderServices.Application.Entities.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("Id");

                    b.Property<string>("OrderId")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<int>("ProductId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ProductId");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("ProductName");

                    b.Property<int>("Quantity")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("Quantity");

                    b.HasKey("Id")
                        .HasName("Tbl_OrderItem_Pk");

                    b.HasIndex("OrderId");

                    b.ToTable("Tbl_OrderItem", (string)null);
                });

            modelBuilder.Entity("OrderServices.Application.Entities.Order", b =>
                {
                    b.HasOne("OrderServices.Application.Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("OrderServices.Application.Entities.OrderItem", b =>
                {
                    b.HasOne("OrderServices.Application.Entities.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("OrderServices.Application.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
