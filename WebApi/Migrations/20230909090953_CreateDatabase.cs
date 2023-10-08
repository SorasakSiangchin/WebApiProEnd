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
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LevelProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelProducts", x => x.Id);
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
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    LoginBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WeightUnitID = table.Column<int>(type: "int", nullable: false),
                    LevelProductID = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Products_LevelProducts_LevelProductID",
                        column: x => x.LevelProductID,
                        principalTable: "LevelProducts",
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
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AddressInformationsId = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                        name: "FK_Addresses_AddressInformations_AddressInformationsId",
                        column: x => x.AddressInformationsId,
                        principalTable: "AddressInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Subtotal = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryFee = table.Column<long>(type: "bigint", nullable: false),
                    CustomerStatus = table.Column<bool>(type: "bit", nullable: false),
                    SellerStatus = table.Column<bool>(type: "bit", nullable: false),
                    OrderCancel = table.Column<bool>(type: "bit", nullable: false),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    OrderUsage = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemOrderedProductID = table.Column<string>(name: "ItemOrdered_ProductID", type: "nvarchar(max)", nullable: false),
                    ItemOrderedName = table.Column<string>(name: "ItemOrdered_Name", type: "nvarchar(max)", nullable: false),
                    ItemOrderedImageUrl = table.Column<string>(name: "ItemOrdered_ImageUrl", type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMessages_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
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
                    Score = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_OrderItems_OrderItemID",
                        column: x => x.OrderItemID,
                        principalTable: "OrderItems",
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
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "มังคุด" },
                    { 3, "ลำไย" },
                    { 4, "ทุเรียน" },
                    { 5, "เงาะ" },
                    { 6, "มะม่วง" },
                    { 7, "กล้วย" },
                    { 8, "มะพร้าว" },
                    { 9, "ขนุน" },
                    { 10, "หมาก" },
                    { 11, "มะละกอ" },
                    { 12, "น้อยหน่า" },
                    { 13, "ลองกอง" },
                    { 14, "ส้ม" },
                    { 15, "ชมพู่" },
                    { 16, "ขนุน" },
                    { 999, "หายาก" }
                });

            migrationBuilder.InsertData(
                table: "LevelProducts",
                columns: new[] { "Id", "Level" },
                values: new object[,]
                {
                    { 1, "หาได้ทั่วไป" },
                    { 2, "ปานกลาง" },
                    { 3, "หายาก" }
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
                table: "StatusDeliverys",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "กำลังเตรียมพัสดุ" },
                    { 2, "บริษัทขนส่งเข้ารับพัสดุเรียบร้อยแล้ว" },
                    { 3, "พัสดุถึงศูนย์คัดแยกสินค้า" },
                    { 4, "พัสดุออกจากศูนย์คัดแยกสินค้า" },
                    { 5, "พัสดุถึงสาขาปลายทาง" },
                    { 999, "การจัดส่งสำเร็จ" }
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
                columns: new[] { "Id", "Email", "FirstName", "ImageUrl", "LastName", "LoginBy", "Password", "PhoneNumber", "RoleID", "Status" },
                values: new object[,]
                {
                    { "account01", "XxArmCustomerxX@gmail.com", "ArmCustomer", "https://cv.lnwfile.com/_/cv/_raw/rn/s7/3p.png", "Kung", "", "Mw52so+8rK3NMGo/MDb85w==.7Rxql67dW+VxjYSja2uusRgJy860Q0KKTD0RyA9ju8Q=", "0616032203", 1, true },
                    { "account02", "XxArmSellerxX@gmail.com", "ArmSeller", "https://cv.lnwfile.com/_/cv/_raw/rn/s7/3p.png", "Kung", "", "Mw52so+8rK3NMGo/MDb85w==.7Rxql67dW+VxjYSja2uusRgJy860Q0KKTD0RyA9ju8Q=", "0616032203", 2, true },
                    { "account03", "XxArmAdminxX@gmail.com", "ArmAdmin", "https://cv.lnwfile.com/_/cv/_raw/rn/s7/3p.png", "Kung", "", "Mw52so+8rK3NMGo/MDb85w==.7Rxql67dW+VxjYSja2uusRgJy860Q0KKTD0RyA9ju8Q=", "0616032203", 3, true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AccountID", "CategoryProductID", "Color", "Created", "Description", "ImageUrl", "LastUpdate", "LevelProductID", "Name", "Price", "Stock", "Weight", "WeightUnitID" },
                values: new object[,]
                {
                    { "product01", "account01", 2, "เขียว", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8866), "อร่อย", "https://www.ตลาดกลางผลไม้.com/wp-content/uploads/2019/10/72414759_106730297406260_6103449987075538944_n.jpg", null, 1, "ส้มโอทับทิมสยาม", 150, 50, 1.0, 1 },
                    { "product02", "account01", 2, "เขียว", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8889), "อร่อย", "https://scontent-sin6-4.xx.fbcdn.net/v/t39.30808-6/299863661_433735048781055_6878774558773361005_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=09cbfe&_nc_ohc=3DcFFDZIL-0AX_oGgNx&_nc_ht=scontent-sin6-4.xx&oh=00_AfDQhZa4WiqXQ5mbCx6057-jHaiFA7Llf5Caf9UuFfyl9w&oe=64EC700D", null, 1, "กล้วยน้ำว้ามะลิอ่อง", 150, 50, 1.0, 1 },
                    { "product03", "account01", 2, "เขียว", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8892), "อร่อย", "https://cdn.shortpixel.ai/spai/w_870+q_lossy+ret_img+to_webp/amprohealth.com/wp-content/uploads/2017/09/135-%E0%B9%80%E0%B8%AA%E0%B8%B2%E0%B8%A7%E0%B8%A3%E0%B8%AA.jpg", null, 1, "เสาวรส", 150, 50, 1.0, 1 },
                    { "product04", "account01", 2, "ม่วง", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8893), "อร่อย", "https://ตลาดเกษตรกรออนไลน์.com/uploads/products/cover_6285c7898575f.jpg", null, 1, "มังคุดสด มาตรฐาน GAP", 250, 50, 1.0, 1 },
                    { "product05", "account01", 2, "เหลืองทอง", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8895), "อร่อย", "https://dk8dmtco5sckz.cloudfront.net/wp-content/uploads/2023/03/AnyConv.com__15-8.webp", null, 1, "ทุเรียนหมอนทอง", 250, 50, 1.0, 1 },
                    { "product06", "account01", 3, "เหลืองทอง", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8897), "อร่อย", "https://www.ตลาดเกษตรกรออนไลน์.com/uploads/products/cover_64914f3b033fd.jpg", null, 1, "แก้วมังกร", 250, 50, 1.0, 1 },
                    { "product07", "account01", 3, "เหลืองทอง", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8899), "หวานฉ่ำชื่นใจ ไม่แสบคอ เนื้อเยอะ เม็ดลีบ แห้งไม่แฉะ", "https://www.ตลาดเกษตรกรออนไลน์.com/uploads/products/cover_64914f3b033fd.jpg", null, 1, "ลำไยอีดอ", 250, 50, 1.0, 1 },
                    { "product08", "account03", 999, "เลือดหมู", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8901), "หวานฉ่ำชื่นใจ ไม่แสบคอ เนื้อเยอะ เม็ดลีบ แห้งไม่แฉะ", "https://www.77kaoded.com/wp-content/uploads/2021/08/IMG_5061.jpg", null, 1, "เสาวรส", 250, 50, 1.0, 1 },
                    { "product09", "account01", 3, "เหลือง", new DateTime(2023, 9, 9, 16, 9, 53, 585, DateTimeKind.Local).AddTicks(8903), "อร่อย", "https://www.ตลาดเกษตรกรออนไลน์.com/uploads/products/398.jpg", null, 1, "เมล่อน", 250, 50, 1.0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleID",
                table: "Accounts",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AccountID",
                table: "Addresses",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressInformationsId",
                table: "Addresses",
                column: "AddressInformationsId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AccountID",
                table: "Carts",
                column: "AccountID");

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
                name: "IX_ImageProducts_ProductID",
                table: "ImageProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageReviews_ReviewID",
                table: "ImageReviews",
                column: "ReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMessages_OrderID",
                table: "OrderMessages",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressID",
                table: "Orders",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryProductID",
                table: "Products",
                column: "CategoryProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LevelProductID",
                table: "Products",
                column: "LevelProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WeightUnitID",
                table: "Products",
                column: "WeightUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderItemID",
                table: "Reviews",
                column: "OrderItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Deliverys");

            migrationBuilder.DropTable(
                name: "DetailProducts");

            migrationBuilder.DropTable(
                name: "EvidenceMoneyTransfers");

            migrationBuilder.DropTable(
                name: "ImageProducts");

            migrationBuilder.DropTable(
                name: "ImageReviews");

            migrationBuilder.DropTable(
                name: "OrderMessages");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "StatusDeliverys");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "CategoryProducts");

            migrationBuilder.DropTable(
                name: "LevelProducts");

            migrationBuilder.DropTable(
                name: "WeightUnits");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AddressInformations");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
