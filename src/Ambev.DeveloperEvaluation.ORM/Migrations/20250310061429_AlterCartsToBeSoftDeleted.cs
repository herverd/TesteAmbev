using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AlterCartsToBeSoftDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Carts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Carts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CartItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "CartItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_DeletedById",
                table: "Carts",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_DeletedById",
                table: "CartItems",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Users_DeletedById",
                table: "CartItems",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_DeletedById",
                table: "Carts",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Users_DeletedById",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_DeletedById",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_DeletedById",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_DeletedById",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "CartItems");
        }
    }
}
