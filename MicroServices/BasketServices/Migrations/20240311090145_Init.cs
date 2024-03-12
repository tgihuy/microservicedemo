using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketServices.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_CustomerBasket",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Tbl_CustomerBasket_Id", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_BasketItem",
                columns: table => new
                {
                    BasketItemId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProductName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Quantity = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CustomerBasketCustomerId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Tbl_BasketItem_Pk", x => x.BasketItemId);
                    table.ForeignKey(
                        name: "FK_Tbl_BasketItem_Tbl_CustomerBasket_CustomerBasketCustomerId",
                        column: x => x.CustomerBasketCustomerId,
                        principalTable: "Tbl_CustomerBasket",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_BasketItem_BasketItemId",
                table: "Tbl_BasketItem",
                column: "BasketItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_BasketItem_CustomerBasketCustomerId",
                table: "Tbl_BasketItem",
                column: "CustomerBasketCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_BasketItem");

            migrationBuilder.DropTable(
                name: "Tbl_CustomerBasket");
        }
    }
}
