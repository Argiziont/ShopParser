using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopParserApi.Migrations
{
    public partial class JsonParseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "AttributeValue",
                "ProductAttributes",
                "AttributeValues");

            migrationBuilder.RenameColumn(
                "Href",
                "Categories",
                "Url");

            migrationBuilder.AddColumn<string>(
                "AttributeGroup",
                "ProductAttributes",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "ExternalId",
                "ProductAttributes",
                "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "AttributeGroup",
                "ProductAttributes");

            migrationBuilder.DropColumn(
                "ExternalId",
                "ProductAttributes");

            migrationBuilder.RenameColumn(
                "AttributeValues",
                "ProductAttributes",
                "AttributeValue");

            migrationBuilder.RenameColumn(
                "Url",
                "Categories",
                "Href");
        }
    }
}