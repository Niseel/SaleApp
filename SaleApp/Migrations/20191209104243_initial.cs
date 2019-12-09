using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Updated_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    PhotoPath = table.Column<string>(type: "nvarchar(255)", nullable: true, defaultValue: "default.png")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false, defaultValue: "This category has no description"),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Updated_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    PhotoPath = table.Column<string>(type: "nvarchar(255)", nullable: true, defaultValue: "default.png")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(45)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    MethodPaying = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    BankBrand = table.Column<string>(type: "nvarchar(45)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(45)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1000)", nullable: true, defaultValue: "This order dont have note"),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    Created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Updated_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Birth = table.Column<DateTime>(type: "DATE", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    AvtPath = table.Column<string>(type: "nvarchar(255)", nullable: true, defaultValue: "default.png"),
                    Note = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Level = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Updated_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Sale = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", nullable: true, defaultValue: "default.png"),
                    Content = table.Column<string>(type: "nvarchar(1000)", nullable: true, defaultValue: "This Product has no content"),
                    View = table.Column<int>(nullable: false, defaultValue: 0),
                    Pay = table.Column<int>(nullable: false, defaultValue: 0),
                    Hot = table.Column<short>(type: "smallint", nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Updated_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailOrders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Updated_at = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailOrders", x => new { x.ID, x.ProductID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_DetailOrders_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailOrders_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_OrderID",
                table: "DetailOrders",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_ProductID",
                table: "DetailOrders",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandID",
                table: "Products",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailOrders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
