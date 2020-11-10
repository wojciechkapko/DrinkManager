using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkManagerWeb.Migrations
{
    public partial class RemoveIsReviewedPropertyFromDrinkEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReviewed",
                table: "Drinks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReviewed",
                table: "Drinks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
