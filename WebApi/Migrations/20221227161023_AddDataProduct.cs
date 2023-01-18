using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDataProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                columns: new[] { "Created", "ImageUrl" },
                values: new object[] { new DateTime(2022, 12, 27, 23, 10, 23, 585, DateTimeKind.Local).AddTicks(8690), "df339981-6e81-4b28-bbb9-bdcb194a05a3.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                columns: new[] { "Created", "ImageUrl" },
                values: new object[] { new DateTime(2022, 12, 27, 23, 10, 23, 585, DateTimeKind.Local).AddTicks(8696), "d6667cbd-f010-43b8-95e0-bf3d8ff218bb.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                columns: new[] { "Created", "ImageUrl" },
                values: new object[] { new DateTime(2022, 12, 27, 23, 10, 23, 585, DateTimeKind.Local).AddTicks(8698), "d3c013ec-f736-4750-86a5-53b0c6136a9c.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                columns: new[] { "Created", "ImageUrl" },
                values: new object[] { new DateTime(2022, 12, 27, 23, 10, 23, 585, DateTimeKind.Local).AddTicks(8700), "be242077-737c-48ae-935d-f0ba03ec7d25.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                columns: new[] { "Created", "ImageUrl" },
                values: new object[] { new DateTime(2022, 12, 10, 13, 20, 23, 978, DateTimeKind.Local).AddTicks(2167), "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                columns: new[] { "Created", "ImageUrl" },
                values: new object[] { new DateTime(2022, 12, 10, 13, 20, 23, 978, DateTimeKind.Local).AddTicks(2172), "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                columns: new[] { "Created", "ImageUrl" },
                values: new object[] { new DateTime(2022, 12, 10, 13, 20, 23, 978, DateTimeKind.Local).AddTicks(2174), "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                columns: new[] { "Created", "ImageUrl" },
                values: new object[] { new DateTime(2022, 12, 10, 13, 20, 23, 978, DateTimeKind.Local).AddTicks(2177), "" });
        }
    }
}
