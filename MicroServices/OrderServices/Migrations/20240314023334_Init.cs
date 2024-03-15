using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderServices.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Customer",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CustomerName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Tbl_Customer_Pk", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Order",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CustomerId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    Street = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    City = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    District = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AdditionalAddress = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Tbl_Order_Pk", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Tbl_Order_Tbl_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Tbl_Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_OrderItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ProductName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OrderId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Tbl_OrderItem_Pk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_OrderItem_Tbl_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Tbl_Order",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Order_CustomerId",
                table: "Tbl_Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_OrderItem_OrderId",
                table: "Tbl_OrderItem",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_OrderItem");

            migrationBuilder.DropTable(
                name: "Tbl_Order");

            migrationBuilder.DropTable(
                name: "Tbl_Customer");
        }
    }
}
