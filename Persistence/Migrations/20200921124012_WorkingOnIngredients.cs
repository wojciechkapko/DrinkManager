using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class WorkingOnIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkIngredients");

            migrationBuilder.AddColumn<string>(
                name: "DrinkId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_DrinkId",
                table: "Ingredients",
                column: "DrinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Drinks_DrinkId",
                table: "Ingredients",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "DrinkId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Drinks_DrinkId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_DrinkId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "DrinkIngredients",
                columns: table => new
                {
                    DrinkId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IngredientId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkIngredients", x => new { x.DrinkId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_DrinkIngredients_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "DrinkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredients_IngredientId",
                table: "DrinkIngredients",
                column: "IngredientId");
        }
    }
}
