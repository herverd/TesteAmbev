using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class InsertDefaultUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchaseStatus",
                table: "Carts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseStatus",
                table: "CartItems",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Phone", "Role", "Status", "UpdatedAt", "Username" },
                values: new object[] { new Guid("03427611-5bd8-40af-9b24-16192d7cace6"), new DateTime(2025, 3, 11, 9, 28, 15, 341, DateTimeKind.Utc).AddTicks(6233), "admin@gmail.com", "$2a$11$uXrL7R90QQ4G1bILyQjFNeoFZZPXG2hDtW37kp2fN.5Gpc5Zg1Qv2", "51981344567", "Admin", "Active", null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("03427611-5bd8-40af-9b24-16192d7cace6"));

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseStatus",
                table: "Carts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseStatus",
                table: "CartItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);
        }
    }
}
