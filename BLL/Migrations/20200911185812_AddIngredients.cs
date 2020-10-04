using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkManagerWeb.Migrations
{
    public partial class AddIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Drinks_DrinkId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_DrinkId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Drinks");

            migrationBuilder.AddColumn<string>(
                name: "IngredientId",
                table: "Ingredients",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DrinkId",
                table: "Drinks",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks",
                column: "DrinkId");

            migrationBuilder.CreateTable(
                name: "DrinkIngredient",
                columns: table => new
                {
                    DrinkId = table.Column<string>(nullable: false),
                    IngredientId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkIngredient", x => new { x.DrinkId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_DrinkIngredient_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "DrinkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkIngredient_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredient_IngredientId",
                table: "DrinkIngredient",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "Drinks");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DrinkId",
                table: "Ingredients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Drinks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_DrinkId",
                table: "Ingredients",
                column: "DrinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Drinks_DrinkId",
                table: "Ingredients",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
