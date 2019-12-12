using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleApp.Migrations
{
    public partial class changeDefaultValueUserPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(45)",
                nullable: false,
                defaultValue: "e10adc3949ba59abbe56e057f20f883e",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(45)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldDefaultValue: "e10adc3949ba59abbe56e057f20f883e");
        }
    }
}
