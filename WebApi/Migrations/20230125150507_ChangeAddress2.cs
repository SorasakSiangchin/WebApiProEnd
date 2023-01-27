using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAddress2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressInformations_Addresses_AddressId",
                table: "AddressInformations");

            migrationBuilder.DropIndex(
                name: "IX_AddressInformations_AddressId",
                table: "AddressInformations");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "AddressInformations");

            migrationBuilder.AddColumn<int>(
                name: "AddressInformationsId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressInformationsId",
                table: "Addresses",
                column: "AddressInformationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AddressInformations_AddressInformationsId",
                table: "Addresses",
                column: "AddressInformationsId",
                principalTable: "AddressInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AddressInformations_AddressInformationsId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_AddressInformationsId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressInformationsId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "AddressInformations",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-01",
                column: "Created",
                value: new DateTime(2023, 1, 23, 16, 43, 14, 804, DateTimeKind.Local).AddTicks(5509));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                column: "Created",
                value: new DateTime(2023, 1, 23, 16, 43, 14, 804, DateTimeKind.Local).AddTicks(5513));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                column: "Created",
                value: new DateTime(2023, 1, 23, 16, 43, 14, 804, DateTimeKind.Local).AddTicks(5515));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                column: "Created",
                value: new DateTime(2023, 1, 23, 16, 43, 14, 804, DateTimeKind.Local).AddTicks(5518));

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformations_AddressId",
                table: "AddressInformations",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressInformations_Addresses_AddressId",
                table: "AddressInformations",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
