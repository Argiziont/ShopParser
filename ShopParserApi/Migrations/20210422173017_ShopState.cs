using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopParserApi.Migrations
{
    public partial class ShopState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopState",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopState",
                table: "Shops");
        }
    }
}
