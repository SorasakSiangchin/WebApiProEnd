using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIDToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "CategoryProducts");

            migrationBuilder.AddColumn<string>(
                name: "AccountID",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CategoryProducts",
                columns: new[] { "Id", "Name" },
                values: new object[] { 999, "rare" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                columns: new[] { "AccountID", "Created" },
                values: new object[] { null, new DateTime(2023, 1, 19, 13, 43, 14, 122, DateTimeKind.Local).AddTicks(4196) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                columns: new[] { "AccountID", "Created" },
                values: new object[] { null, new DateTime(2023, 1, 19, 13, 43, 14, 122, DateTimeKind.Local).AddTicks(4202) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                columns: new[] { "AccountID", "Created" },
                values: new object[] { null, new DateTime(2023, 1, 19, 13, 43, 14, 122, DateTimeKind.Local).AddTicks(4204) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                columns: new[] { "AccountID", "Created" },
                values: new object[] { null, new DateTime(2023, 1, 19, 13, 43, 14, 122, DateTimeKind.Local).AddTicks(4206) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryProducts",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "AccountID",
                table: "CategoryProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CategoryProducts",
                keyColumn: "Id",
                keyValue: 2,
                column: "AccountID",
                value: "account-01");

            migrationBuilder.UpdateData(
                table: "CategoryProducts",
                keyColumn: "Id",
                keyValue: 3,
                column: "AccountID",
                value: "account-02");

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
        }
    }
}
