using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddImageProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageProducts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                column: "Created",
                value: new DateTime(2022, 12, 10, 13, 20, 23, 978, DateTimeKind.Local).AddTicks(2167));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                column: "Created",
                value: new DateTime(2022, 12, 10, 13, 20, 23, 978, DateTimeKind.Local).AddTicks(2172));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                column: "Created",
                value: new DateTime(2022, 12, 10, 13, 20, 23, 978, DateTimeKind.Local).AddTicks(2174));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                column: "Created",
                value: new DateTime(2022, 12, 10, 13, 20, 23, 978, DateTimeKind.Local).AddTicks(2177));

            migrationBuilder.CreateIndex(
                name: "IX_ImageProducts_ProductID",
                table: "ImageProducts",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageProducts");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                column: "Created",
                value: new DateTime(2022, 12, 8, 15, 37, 50, 526, DateTimeKind.Local).AddTicks(2536));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                column: "Created",
                value: new DateTime(2022, 12, 8, 15, 37, 50, 526, DateTimeKind.Local).AddTicks(2552));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                column: "Created",
                value: new DateTime(2022, 12, 8, 15, 37, 50, 526, DateTimeKind.Local).AddTicks(2554));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                column: "Created",
                value: new DateTime(2022, 12, 8, 15, 37, 50, 526, DateTimeKind.Local).AddTicks(2556));
        }
    }
}
