using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtClubVT.Data.Migrations
{
    public partial class RemoveProductCodeFromItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCode",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
