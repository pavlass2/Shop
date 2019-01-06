﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp;

namespace WebApp.DataContext.ItemMigrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20181231015501_AddedLanguagesAndCategories")]
    partial class AddedLanguagesAndCategories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApp.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<int>("Category");

                    b.Property<string>("Descriotion");

                    b.Property<string>("EncryptedId");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("Language");

                    b.Property<string>("PicturePath");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("WebApp.Models.Author", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("WebApp.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressCity");

                    b.Property<string>("AddressCountry");

                    b.Property<string>("AddressHouseNumber");

                    b.Property<string>("AddressStreet");

                    b.Property<string>("AddressZip");

                    b.Property<int>("CustomerId");

                    b.Property<string>("CustomerName");

                    b.Property<DateTime>("DateTimeDelivered");

                    b.Property<DateTime>("DateTimeDispatched");

                    b.Property<DateTime>("DateTimeOrdered");

                    b.Property<int>("Delivery");

                    b.Property<string>("Email");

                    b.Property<string>("EncryptedId");

                    b.Property<string>("FirstName");

                    b.Property<string>("Items");

                    b.Property<int>("Payment");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SecondName");

                    b.Property<int>("State");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
