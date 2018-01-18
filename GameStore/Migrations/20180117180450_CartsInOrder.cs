using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameStore.Migrations
{
    public partial class CartsInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Cart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Cart",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_OrderId",
                table: "Cart",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Order_OrderId",
                table: "Cart",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Order_OrderId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_OrderId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Cart");
        }
    }
}
