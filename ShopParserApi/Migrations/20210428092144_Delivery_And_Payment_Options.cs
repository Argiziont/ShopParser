using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopParserApi.Migrations
{
    public partial class Delivery_And_Payment_Options : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_companies_Sources_SourceId",
                "companies");

            migrationBuilder.DropForeignKey(
                "FK_Products_companies_companyId",
                "Products");

            migrationBuilder.DropPrimaryKey(
                "PK_companies",
                "companies");

            migrationBuilder.RenameTable(
                "companies",
                newName: "Companies");

            migrationBuilder.RenameColumn(
                "companyId",
                "Products",
                "CompanyId");

            migrationBuilder.RenameIndex(
                "IX_Products_companyId",
                table: "Products",
                newName: "IX_Products_CompanyId");

            migrationBuilder.RenameColumn(
                "companyState",
                "Companies",
                "CompanyState");

            migrationBuilder.RenameIndex(
                "IX_companies_SourceId",
                table: "Companies",
                newName: "IX_Companies_SourceId");

            migrationBuilder.AddPrimaryKey(
                "PK_Companies",
                "Companies",
                "Id");

            migrationBuilder.CreateTable(
                "Presence",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresenceSureAvailable = table.Column<bool>("bit", nullable: false),
                    OrderAvailable = table.Column<bool>("bit", nullable: false),
                    Available = table.Column<bool>("bit", nullable: false),
                    Title = table.Column<string>("nvarchar(max)", nullable: true),
                    Ending = table.Column<bool>("bit", nullable: false),
                    Waiting = table.Column<bool>("bit", nullable: false),
                    ProductId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presence", x => x.Id);
                    table.ForeignKey(
                        "FK_Presence_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ProductDeliveryOption",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>("int", nullable: true),
                    ExternalId = table.Column<int>("int", nullable: false),
                    OptionName = table.Column<string>("nvarchar(max)", nullable: true),
                    OptionsComment = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDeliveryOptions", x => x.Id);
                    table.ForeignKey(
                        "FK_ProductDeliveryOptions_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "ProductPaymentOption",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>("int", nullable: true),
                    ExternalId = table.Column<int>("int", nullable: false),
                    OptionName = table.Column<string>("nvarchar(max)", nullable: true),
                    OptionsComment = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPaymentOptions", x => x.Id);
                    table.ForeignKey(
                        "FK_ProductPaymentOptions_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Presence_ProductId",
                "Presence",
                "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_ProductDeliveryOptions_ProductId",
                "ProductDeliveryOption",
                "ProductId");

            migrationBuilder.CreateIndex(
                "IX_ProductPaymentOptions_ProductId",
                "ProductPaymentOption",
                "ProductId");

            migrationBuilder.AddForeignKey(
                "FK_Companies_Sources_SourceId",
                "Companies",
                "SourceId",
                "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Products_Companies_CompanyId",
                "Products",
                "CompanyId",
                "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Companies_Sources_SourceId",
                "Companies");

            migrationBuilder.DropForeignKey(
                "FK_Products_Companies_CompanyId",
                "Products");

            migrationBuilder.DropTable(
                "Presence");

            migrationBuilder.DropTable(
                "ProductDeliveryOption");

            migrationBuilder.DropTable(
                "ProductPaymentOption");

            migrationBuilder.DropPrimaryKey(
                "PK_Companies",
                "Companies");

            migrationBuilder.RenameTable(
                "Companies",
                newName: "companies");

            migrationBuilder.RenameColumn(
                "CompanyId",
                "Products",
                "companyId");

            migrationBuilder.RenameIndex(
                "IX_Products_CompanyId",
                table: "Products",
                newName: "IX_Products_companyId");

            migrationBuilder.RenameColumn(
                "CompanyState",
                "companies",
                "companyState");

            migrationBuilder.RenameIndex(
                "IX_Companies_SourceId",
                table: "companies",
                newName: "IX_companies_SourceId");

            migrationBuilder.AddPrimaryKey(
                "PK_companies",
                "companies",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_companies_Sources_SourceId",
                "companies",
                "SourceId",
                "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Products_companies_companyId",
                "Products",
                "companyId",
                "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}