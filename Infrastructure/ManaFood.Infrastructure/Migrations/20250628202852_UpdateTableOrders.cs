using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManaFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_products_Products_product_id",
                table: "order_products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_products",
                table: "order_products");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                table: "Orders",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "order_time",
                table: "Orders",
                newName: "order_confirmation_time");

            migrationBuilder.AlterColumn<int>(
                name: "payment_method",
                table: "Orders",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "order_products",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "order_products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "order_products",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "order_products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "order_products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "order_products",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<double>(
                name: "quantity",
                table: "order_products",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_products",
                table: "order_products",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_order_products_order_id_product_id",
                table: "order_products",
                columns: new[] { "order_id", "product_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_Products_product_id",
                table: "order_products",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_products_Products_product_id",
                table: "order_products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_products",
                table: "order_products");

            migrationBuilder.DropIndex(
                name: "IX_order_products_order_id_product_id",
                table: "order_products");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "order_products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "order_products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "order_products");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "order_products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "order_products");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "order_products");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "order_products");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Orders",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "order_confirmation_time",
                table: "Orders",
                newName: "order_time");

            migrationBuilder.AlterColumn<string>(
                name: "payment_method",
                table: "Orders",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_products",
                table: "order_products",
                columns: new[] { "order_id", "product_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_order_products_Products_product_id",
                table: "order_products",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
