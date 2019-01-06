﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.DataContext.ItemMigrations
{
    public partial class PriceChangedToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Items",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
