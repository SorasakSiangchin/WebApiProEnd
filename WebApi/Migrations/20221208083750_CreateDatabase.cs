using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusDeliverys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusDeliverys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeightUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WeightUnitID = table.Column<int>(type: "int", nullable: false),
                    CategoryProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_CategoryProducts_CategoryProductID",
                        column: x => x.CategoryProductID,
                        principalTable: "CategoryProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_WeightUnits_WeightUnitID",
                        column: x => x.WeightUnitID,
                        principalTable: "WeightUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountPasswords_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressInformationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_AddressInformations_AddressInformationID",
                        column: x => x.AddressInformationID,
                        principalTable: "AddressInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpeciesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FertilizeMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlantingMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrowingSeason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HarvestTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PriceTotal = table.Column<int>(type: "int", nullable: false),
                    CustomerStatus = table.Column<bool>(type: "bit", nullable: false),
                    SellerStatus = table.Column<bool>(type: "bit", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deliverys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeArrive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusDeliveryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliverys_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliverys_StatusDeliverys_StatusDeliveryID",
                        column: x => x.StatusDeliveryID,
                        principalTable: "StatusDeliverys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvidenceMoneyTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidenceMoneyTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvidenceMoneyTransfers_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VdoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    score = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListOrderID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_ListOrders_ListOrderID",
                        column: x => x.ListOrderID,
                        principalTable: "ListOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageReviews_Reviews_ReviewID",
                        column: x => x.ReviewID,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryProducts",
                columns: new[] { "Id", "AccountID", "Name" },
                values: new object[,]
                {
                    { 2, "account-01", "category-01" },
                    { 3, "account-02", "category-02" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "customer" },
                    { 2, "seller" },
                    { 3, "admin" }
                });

            migrationBuilder.InsertData(
                table: "WeightUnits",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ลูก" },
                    { 2, "กิโลกรัม" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "ImageUrl", "LastName", "Password", "PhoneNumber", "RoleID" },
                values: new object[,]
                {
                    { "account-01", "Sorasak@gmail.com", "Sorasak", "", "Siangchin", "1233211213", "0616032203", 1 },
                    { "account-02", "Anirut@gmail.com", "Anirut", "", "Chairuen", "4566544546", "0927680099", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryProductID", "Color", "Created", "Description", "ImageUrl", "LastUpdate", "Name", "Price", "Stock", "Weight", "WeightUnitID" },
                values: new object[,]
                {
                    { "product-01", 2, "red", new DateTime(2022, 12, 8, 15, 37, 50, 526, DateTimeKind.Local).AddTicks(2536), "", "", null, "Product01", 100, 5, 20.0, 1 },
                    { "product-02", 2, "green", new DateTime(2022, 12, 8, 15, 37, 50, 526, DateTimeKind.Local).AddTicks(2552), "", "", null, "Product02", 200, 6, 10.0, 2 },
                    { "product-03", 2, "blue", new DateTime(2022, 12, 8, 15, 37, 50, 526, DateTimeKind.Local).AddTicks(2554), "", "", null, "Product03", 300, 7, 30.0, 1 },
                    { "product-04", 2, "black", new DateTime(2022, 12, 8, 15, 37, 50, 526, DateTimeKind.Local).AddTicks(2556), "", "", null, "Product04", 400, 8, 40.0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPasswords_AccountID",
                table: "AccountPasswords",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleID",
                table: "Accounts",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AccountID",
                table: "Addresses",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressInformationID",
                table: "Addresses",
                column: "AddressInformationID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AccountID",
                table: "Carts",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductID",
                table: "Carts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverys_OrderID",
                table: "Deliverys",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverys_StatusDeliveryID",
                table: "Deliverys",
                column: "StatusDeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailProducts_ProductID",
                table: "DetailProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_EvidenceMoneyTransfers_OrderID",
                table: "EvidenceMoneyTransfers",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageReviews_ReviewID",
                table: "ImageReviews",
                column: "ReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_ListOrders_OrderID",
                table: "ListOrders",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ListOrders_ProductID",
                table: "ListOrders",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressID",
                table: "Orders",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryProductID",
                table: "Products",
                column: "CategoryProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WeightUnitID",
                table: "Products",
                column: "WeightUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ListOrderID",
                table: "Reviews",
                column: "ListOrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPasswords");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Deliverys");

            migrationBuilder.DropTable(
                name: "DetailProducts");

            migrationBuilder.DropTable(
                name: "EvidenceMoneyTransfers");

            migrationBuilder.DropTable(
                name: "ImageReviews");

            migrationBuilder.DropTable(
                name: "StatusDeliverys");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ListOrders");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CategoryProducts");

            migrationBuilder.DropTable(
                name: "WeightUnits");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AddressInformations");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
