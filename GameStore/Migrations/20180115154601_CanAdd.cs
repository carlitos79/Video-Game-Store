using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameStore.Migrations
{
    public partial class CanAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderShoppingCartId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanAdd",
                table: "Cart",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalTotal",
                table: "Cart",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderShoppingCartId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CanAdd",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "FinalTotal",
                table: "Cart");
        }
    }
}
