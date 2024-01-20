using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INMAR.Service.Migrations
{
    /// <inheritdoc />
    public partial class createproductstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "users");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    InStock = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<long>(type: "INTEGER", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<long>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
