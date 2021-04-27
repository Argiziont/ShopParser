using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopParserApi.Migrations
{
    public partial class JsonParseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttributeValue",
                table: "ProductAttributes",
                newName: "AttributeValues");

            migrationBuilder.RenameColumn(
                name: "Href",
                table: "Categories",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "AttributeGroup",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "ProductAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeGroup",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "ProductAttributes");

            migrationBuilder.RenameColumn(
                name: "AttributeValues",
                table: "ProductAttributes",
                newName: "AttributeValue");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Categories",
                newName: "Href");
        }
    }
}
