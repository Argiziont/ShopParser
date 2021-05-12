using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopParserApi.Migrations
{
    public partial class CompanyState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "companyState",
                "companies",
                "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "companyState",
                "companies");
        }
    }
}