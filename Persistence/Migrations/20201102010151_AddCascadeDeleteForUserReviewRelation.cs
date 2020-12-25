using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddCascadeDeleteForUserReviewRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_AppUserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AppUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Reviews",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AuthorId",
                table: "Reviews",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_AuthorId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AuthorId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AppUserId",
                table: "Reviews",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_AppUserId",
                table: "Reviews",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
