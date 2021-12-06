using Microsoft.EntityFrameworkCore.Migrations;

namespace SM.Infrastructure.Migrations
{
    public partial class addratingtocomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                schema: "dbo",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "dbo",
                table: "Comments");
        }
    }
}
