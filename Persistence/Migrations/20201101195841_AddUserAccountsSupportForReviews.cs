using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddUserAccountsSupportForReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_DrinkId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DrinkId",
                table: "Reviews",
                column: "DrinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_DrinkId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DrinkId",
                table: "Reviews",
                column: "DrinkId",
                unique: true,
                filter: "[DrinkId] IS NOT NULL");
        }
    }
}
