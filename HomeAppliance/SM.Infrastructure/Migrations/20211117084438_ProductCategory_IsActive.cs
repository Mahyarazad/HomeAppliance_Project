using Microsoft.EntityFrameworkCore.Migrations;

namespace SM.Infrastructure.Migrations
{
    public partial class ProductCategory_IsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInStock",
                schema: "dbo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                schema: "dbo",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "ProductCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "ProductCategories");

            migrationBuilder.AddColumn<bool>(
                name: "IsInStock",
                schema: "dbo",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                schema: "dbo",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
