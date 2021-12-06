using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.Migrations
{
    public partial class updatedOrderAndPaymentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderAndPayments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderToken",
                table: "OrderAndPayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ProductQuantity",
                table: "OrderAndPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderAndPayments_ProductId",
                table: "OrderAndPayments",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAndPayments_Products_ProductId",
                table: "OrderAndPayments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderAndPayments_Products_ProductId",
                table: "OrderAndPayments");

            migrationBuilder.DropIndex(
                name: "IX_OrderAndPayments_ProductId",
                table: "OrderAndPayments");

            migrationBuilder.DropColumn(
                name: "ProductQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderToken",
                table: "OrderAndPayments");

            migrationBuilder.DropColumn(
                name: "ProductQuantity",
                table: "OrderAndPayments");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderAndPayments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
