using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.DataContext.ItemMigrations
{
    public partial class _20180922 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncryptedId",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedId",
                table: "Items");
        }
    }
}
