using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ResetNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AddressInformations_AddressInformationID",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ListOrders_ListOrderID",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "ListOrders");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ListOrderID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_AddressInformationID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ListOrderID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AddressInformationID",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductId",
                table: "OrderItem",
                newName: "ItemOrdered_ProductID");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItem",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_PictureUrl",
                table: "OrderItem",
                newName: "ItemOrdered_ImageUrl");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemID",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OrderCancel",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "OrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_ProductID",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "IX_Reviews_OrderItemID",
                table: "Reviews",
                column: "OrderItemID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_OrderItem_OrderItemID",
                table: "Reviews",
                column: "OrderItemID",
                principalTable: "OrderItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressInformations_Addresses_AddressId",
                table: "AddressInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_OrderItem_OrderItemID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OrderItemID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_AddressInformations_AddressId",
                table: "AddressInformations");

            migrationBuilder.DropColumn(
                name: "OrderItemID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "OrderCancel",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "AddressInformations");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductID",
                table: "OrderItem",
                newName: "ItemOrdered_ProductId");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ImageUrl",
                table: "OrderItem",
                newName: "ItemOrdered_PictureUrl");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderItem",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "ListOrderID",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                table: "OrderItem",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemOrdered_ProductId",
                table: "OrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AddressInformationID",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ListOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListOrders_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListOrders_Products_ProductID",
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
                value: new DateTime(2023, 1, 22, 23, 13, 51, 335, DateTimeKind.Local).AddTicks(5851));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-02",
                column: "Created",
                value: new DateTime(2023, 1, 22, 23, 13, 51, 335, DateTimeKind.Local).AddTicks(5876));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-03",
                column: "Created",
                value: new DateTime(2023, 1, 22, 23, 13, 51, 335, DateTimeKind.Local).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "product-04",
                column: "Created",
                value: new DateTime(2023, 1, 22, 23, 13, 51, 335, DateTimeKind.Local).AddTicks(5905));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ListOrderID",
                table: "Reviews",
                column: "ListOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressInformationID",
                table: "Addresses",
                column: "AddressInformationID");

            migrationBuilder.CreateIndex(
                name: "IX_ListOrders_OrderID",
                table: "ListOrders",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ListOrders_ProductID",
                table: "ListOrders",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AddressInformations_AddressInformationID",
                table: "Addresses",
                column: "AddressInformationID",
                principalTable: "AddressInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ListOrders_ListOrderID",
                table: "Reviews",
                column: "ListOrderID",
                principalTable: "ListOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
