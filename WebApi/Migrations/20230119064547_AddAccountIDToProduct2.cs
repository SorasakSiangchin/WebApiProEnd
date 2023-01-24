using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIDToProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountID",
                table: "Products",
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
                columns: new[] { "AccountID", "Created" },
                values: new object[] { "account-01", new DateTime(2023, 1, 19, 13, 45, 47, 660, DateTimeKind.Local).AddTicks(1598) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                columns: new[] { "AccountID", "Created" },
                values: new object[] { "account-01", new DateTime(2023, 1, 19, 13, 45, 47, 660, DateTimeKind.Local).AddTicks(1608) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                columns: new[] { "AccountID", "Created" },
                values: new object[] { "account-01", new DateTime(2023, 1, 19, 13, 45, 47, 660, DateTimeKind.Local).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                columns: new[] { "AccountID", "Created" },
                values: new object[] { "account-01", new DateTime(2023, 1, 19, 13, 45, 47, 660, DateTimeKind.Local).AddTicks(1613) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountID",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
