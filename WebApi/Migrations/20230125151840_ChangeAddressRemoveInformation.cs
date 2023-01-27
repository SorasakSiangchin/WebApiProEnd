using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAddressRemoveInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "AddressInformations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AddressInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                column: "Created",
                value: new DateTime(2023, 1, 25, 22, 18, 39, 540, DateTimeKind.Local).AddTicks(9331));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                column: "Created",
                value: new DateTime(2023, 1, 25, 22, 18, 39, 540, DateTimeKind.Local).AddTicks(9338));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                column: "Created",
                value: new DateTime(2023, 1, 25, 22, 18, 39, 540, DateTimeKind.Local).AddTicks(9342));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                column: "Created",
                value: new DateTime(2023, 1, 25, 22, 18, 39, 540, DateTimeKind.Local).AddTicks(9345));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AddressInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "AddressInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                column: "Created",
                value: new DateTime(2023, 1, 25, 22, 5, 7, 52, DateTimeKind.Local).AddTicks(629));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                column: "Created",
                value: new DateTime(2023, 1, 25, 22, 5, 7, 52, DateTimeKind.Local).AddTicks(638));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                column: "Created",
                value: new DateTime(2023, 1, 25, 22, 5, 7, 52, DateTimeKind.Local).AddTicks(642));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                column: "Created",
                value: new DateTime(2023, 1, 25, 22, 5, 7, 52, DateTimeKind.Local).AddTicks(646));
        }
    }
}
