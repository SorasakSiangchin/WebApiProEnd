using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeDataToCartItemProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId1",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                column: "Created",
                value: new DateTime(2023, 1, 7, 21, 15, 45, 633, DateTimeKind.Local).AddTicks(2289));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                column: "Created",
                value: new DateTime(2023, 1, 7, 21, 15, 45, 633, DateTimeKind.Local).AddTicks(2293));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                column: "Created",
                value: new DateTime(2023, 1, 7, 21, 15, 45, 633, DateTimeKind.Local).AddTicks(2295));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                column: "Created",
                value: new DateTime(2023, 1, 7, 21, 15, 45, 633, DateTimeKind.Local).AddTicks(2297));

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                column: "Created",
                value: new DateTime(2023, 1, 7, 11, 55, 29, 677, DateTimeKind.Local).AddTicks(8201));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                column: "Created",
                value: new DateTime(2023, 1, 7, 11, 55, 29, 677, DateTimeKind.Local).AddTicks(8206));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                column: "Created",
                value: new DateTime(2023, 1, 7, 11, 55, 29, 677, DateTimeKind.Local).AddTicks(8208));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                column: "Created",
                value: new DateTime(2023, 1, 7, 11, 55, 29, 677, DateTimeKind.Local).AddTicks(8210));

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId1",
                table: "CartItems",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId1",
                table: "CartItems",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
