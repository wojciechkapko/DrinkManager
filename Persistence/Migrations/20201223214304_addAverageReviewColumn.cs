using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class addAverageReviewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageReview",
                table: "Drinks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(
                @"CREATE TRIGGER dbo.Calculate_average ON dbo.Reviews

                AFTER INSERT
                AS
                BEGIN

                SET NOCOUNT ON;
                DECLARE @Id NVARCHAR(450)

                SELECT @Id = INSERTED.DrinkId
                FROM INSERTED


                UPDATE dbo.Drinks
                SET dbo.Drinks.AverageReview = (SELECT avg(ReviewScore) FROM dbo.Reviews WHERE dbo.Reviews.DrinkId = @Id)
                WHERE dbo.Drinks.DrinkId = @Id
                END"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER dbo.Calculate_average");

            migrationBuilder.DropColumn(
                name: "AverageReview",
                table: "Drinks");
        }
    }
}
