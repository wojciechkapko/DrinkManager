using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkManagerWeb.Migrations
{
    public partial class AddIsFavouriteProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Reviews_DrinkReviewId",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "DrinkReview");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkReview",
                table: "DrinkReview",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_DrinkReview_DrinkReviewId",
                table: "Drinks",
                column: "DrinkReviewId",
                principalTable: "DrinkReview",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_DrinkReview_DrinkReviewId",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkReview",
                table: "DrinkReview");

            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Drinks");

            migrationBuilder.RenameTable(
                name: "DrinkReview",
                newName: "Reviews");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Reviews_DrinkReviewId",
                table: "Drinks",
                column: "DrinkReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
