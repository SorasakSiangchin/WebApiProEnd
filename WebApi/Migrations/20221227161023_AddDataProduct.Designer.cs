﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiProjectEnd.Modes;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221227161023_AddDataProduct")]
    partial class AddDataProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Modes.ImageProduct", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.ToTable("ImageProducts");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleID");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = "account-01",
                            Email = "Sorasak@gmail.com",
                            FirstName = "Sorasak",
                            ImageUrl = "",
                            LastName = "Siangchin",
                            Password = "1233211213",
                            PhoneNumber = "0616032203",
                            RoleID = 1
                        },
                        new
                        {
                            Id = "account-02",
                            Email = "Anirut@gmail.com",
                            FirstName = "Anirut",
                            ImageUrl = "",
                            LastName = "Chairuen",
                            Password = "4566544546",
                            PhoneNumber = "0927680099",
                            RoleID = 2
                        });
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.AccountPassword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountID");

                    b.ToTable("AccountPasswords");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AddressInformationID")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AccountID");

                    b.HasIndex("AddressInformationID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.AddressInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubDistrict")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AddressInformations");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Cart", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AccountID");

                    b.HasIndex("ProductID");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.CategoryProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryProducts");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            AccountID = "account-01",
                            Name = "category-01"
                        },
                        new
                        {
                            Id = 3,
                            AccountID = "account-02",
                            Name = "category-02"
                        });
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("OrderID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShippingServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusDeliveryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeArrive")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderID");

                    b.HasIndex("StatusDeliveryID");

                    b.ToTable("Deliverys");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.DetailProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FertilizeMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GrowingSeason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HarvestTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlantingMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SpeciesName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.ToTable("DetailProducts");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.EvidenceMoneyTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Evidence")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OrderID");

                    b.ToTable("EvidenceMoneyTransfers");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.ImageReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReviewID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReviewID");

                    b.ToTable("ImageReviews");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.ListOrder", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("OrderID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("ListOrders");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CustomerStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("bit");

                    b.Property<int>("PriceTotal")
                        .HasColumnType("int");

                    b.Property<bool>("SellerStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddressID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CategoryProductID")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.Property<int>("WeightUnitID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryProductID");

                    b.HasIndex("WeightUnitID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = "product-01",
                            CategoryProductID = 2,
                            Color = "red",
                            Created = new DateTime(2022, 12, 27, 23, 10, 23, 585, DateTimeKind.Local).AddTicks(8690),
                            Description = "",
                            ImageUrl = "df339981-6e81-4b28-bbb9-bdcb194a05a3.jpg",
                            Name = "Product01",
                            Price = 100,
                            Stock = 5,
                            Weight = 20.0,
                            WeightUnitID = 1
                        },
                        new
                        {
                            Id = "product-02",
                            CategoryProductID = 2,
                            Color = "green",
                            Created = new DateTime(2022, 12, 27, 23, 10, 23, 585, DateTimeKind.Local).AddTicks(8696),
                            Description = "",
                            ImageUrl = "d6667cbd-f010-43b8-95e0-bf3d8ff218bb.jpg",
                            Name = "Product02",
                            Price = 200,
                            Stock = 6,
                            Weight = 10.0,
                            WeightUnitID = 2
                        },
                        new
                        {
                            Id = "product-03",
                            CategoryProductID = 2,
                            Color = "blue",
                            Created = new DateTime(2022, 12, 27, 23, 10, 23, 585, DateTimeKind.Local).AddTicks(8698),
                            Description = "",
                            ImageUrl = "d3c013ec-f736-4750-86a5-53b0c6136a9c.jpg",
                            Name = "Product03",
                            Price = 300,
                            Stock = 7,
                            Weight = 30.0,
                            WeightUnitID = 1
                        },
                        new
                        {
                            Id = "product-04",
                            CategoryProductID = 2,
                            Color = "black",
                            Created = new DateTime(2022, 12, 27, 23, 10, 23, 585, DateTimeKind.Local).AddTicks(8700),
                            Description = "",
                            ImageUrl = "be242077-737c-48ae-935d-f0ba03ec7d25.jpg",
                            Name = "Product04",
                            Price = 400,
                            Stock = 8,
                            Weight = 40.0,
                            WeightUnitID = 2
                        });
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Information")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListOrderID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VdoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("score")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ListOrderID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "customer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "seller"
                        },
                        new
                        {
                            Id = 3,
                            Name = "admin"
                        });
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.StatusDelivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusDeliverys");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.WeightUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WeightUnits");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ลูก"
                        },
                        new
                        {
                            Id = 2,
                            Name = "กิโลกรัม"
                        });
                });

            modelBuilder.Entity("WebApi.Modes.ImageProduct", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Account", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.AccountPassword", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Address", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiProjectEnd.Modes.AddressInformation", "AddressInformation")
                        .WithMany()
                        .HasForeignKey("AddressInformationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("AddressInformation");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Cart", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiProjectEnd.Modes.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Delivery", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiProjectEnd.Modes.StatusDelivery", "StatusDelivery")
                        .WithMany()
                        .HasForeignKey("StatusDeliveryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("StatusDelivery");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.DetailProduct", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.EvidenceMoneyTransfer", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.ImageReview", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Review", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.ListOrder", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiProjectEnd.Modes.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Order", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Product", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.CategoryProduct", "CategoryProduct")
                        .WithMany()
                        .HasForeignKey("CategoryProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiProjectEnd.Modes.WeightUnit", "WeightUnit")
                        .WithMany()
                        .HasForeignKey("WeightUnitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryProduct");

                    b.Navigation("WeightUnit");
                });

            modelBuilder.Entity("WebApiProjectEnd.Modes.Review", b =>
                {
                    b.HasOne("WebApiProjectEnd.Modes.ListOrder", "ListOrder")
                        .WithMany()
                        .HasForeignKey("ListOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ListOrder");
                });
#pragma warning restore 612, 618
        }
    }
}
