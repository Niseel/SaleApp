using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleApp.Migrations
{
    public partial class changeNOTNULLtblUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birth",
                table: "Users",
                type: "DATE",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "DATE");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(45)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birth",
                table: "Users",
                type: "DATE",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);
        }
    }
}
