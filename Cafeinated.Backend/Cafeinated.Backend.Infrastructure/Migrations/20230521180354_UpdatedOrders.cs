using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafeinated.Backend.Infrastructure.Migrations
{
    public partial class UpdatedOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderCoffeeTypes");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "Orders",
                newName: "CoffeeShopId");

            migrationBuilder.RenameColumn(
                name: "OtherMentions",
                table: "Orders",
                newName: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CoffeeShopId",
                table: "Orders",
                column: "CoffeeShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CoffeeShops_CoffeeShopId",
                table: "Orders",
                column: "CoffeeShopId",
                principalTable: "CoffeeShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CoffeeShops_CoffeeShopId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CoffeeShopId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "TotalCost");

            migrationBuilder.RenameColumn(
                name: "CoffeeShopId",
                table: "Orders",
                newName: "ShippingAddress");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "OtherMentions");

            migrationBuilder.CreateTable(
                name: "OrderCoffeeTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CoffeeTypeId = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCoffeeTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderCoffeeTypes_CoffeeTypes_CoffeeTypeId",
                        column: x => x.CoffeeTypeId,
                        principalTable: "CoffeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCoffeeTypes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderCoffeeTypes_CoffeeTypeId",
                table: "OrderCoffeeTypes",
                column: "CoffeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCoffeeTypes_OrderId",
                table: "OrderCoffeeTypes",
                column: "OrderId");
        }
    }
}
