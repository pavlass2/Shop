using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<string>(nullable: true),
                    EncryptedId = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(maxLength: 255, nullable: false),
                    Language = table.Column<int>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Descriotion = table.Column<string>(nullable: true),
                    PicturePath = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EncryptedId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    Items = table.Column<string>(nullable: true),
                    DateTimeOrdered = table.Column<DateTime>(nullable: false),
                    DateTimeDispatched = table.Column<DateTime>(nullable: false),
                    DateTimeDelivered = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Delivery = table.Column<int>(nullable: false),
                    Payment = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    AddressStreet = table.Column<string>(nullable: true),
                    AddressHouseNumber = table.Column<string>(nullable: true),
                    AddressCity = table.Column<string>(nullable: true),
                    AddressCountry = table.Column<string>(nullable: true),
                    AddressZip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
