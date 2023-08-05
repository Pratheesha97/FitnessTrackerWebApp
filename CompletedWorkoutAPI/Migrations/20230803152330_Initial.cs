using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsumedCheatMealAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumedCheatMeals",
                columns: table => new
                {
                    ConsumedCheatMealId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    AmountConsumed = table.Column<int>(type: "int", nullable: false),
                    PostMealWeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedCheatMeals", x => x.ConsumedCheatMealId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumedCheatMeals");
        }
    }
}
