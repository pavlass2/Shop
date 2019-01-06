using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.DataContext.ItemMigrations
{
    public partial class AddedUsersDataToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressCity",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressCountry",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressHouseNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressStreet",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressZip",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressCountry",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressHouseNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressStreet",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressZip",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "Orders");
        }
    }
}
