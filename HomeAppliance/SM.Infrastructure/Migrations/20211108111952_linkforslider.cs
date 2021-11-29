using Microsoft.EntityFrameworkCore.Migrations;

namespace SM.Infrastructure.Migrations
{
    public partial class linkforslider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BtnText",
                schema: "dbo",
                table: "Slider",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                schema: "dbo",
                table: "Slider",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                schema: "dbo",
                table: "Slider");

            migrationBuilder.AlterColumn<string>(
                name: "BtnText",
                schema: "dbo",
                table: "Slider",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
