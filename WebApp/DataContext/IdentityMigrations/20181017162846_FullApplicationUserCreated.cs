using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.DataContext.IdentityMigrations
{
    public partial class FullApplicationUserCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdressZip",
                table: "AspNetUsers",
                newName: "AddressZip");

            migrationBuilder.RenameColumn(
                name: "AdressStreet",
                table: "AspNetUsers",
                newName: "AddressStreet");

            migrationBuilder.RenameColumn(
                name: "AdressHouseNumber",
                table: "AspNetUsers",
                newName: "AddressHouseNumber");

            migrationBuilder.RenameColumn(
                name: "AdressCountry",
                table: "AspNetUsers",
                newName: "AddressCountry");

            migrationBuilder.RenameColumn(
                name: "AdressCity",
                table: "AspNetUsers",
                newName: "AddressCity");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressZip",
                table: "AspNetUsers",
                newName: "AdressZip");

            migrationBuilder.RenameColumn(
                name: "AddressStreet",
                table: "AspNetUsers",
                newName: "AdressStreet");

            migrationBuilder.RenameColumn(
                name: "AddressHouseNumber",
                table: "AspNetUsers",
                newName: "AdressHouseNumber");

            migrationBuilder.RenameColumn(
                name: "AddressCountry",
                table: "AspNetUsers",
                newName: "AdressCountry");

            migrationBuilder.RenameColumn(
                name: "AddressCity",
                table: "AspNetUsers",
                newName: "AdressCity");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
