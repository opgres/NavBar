using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NavBar.Migrations
{
    /// <inheritdoc />
    public partial class CheckNullableForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_TypeDrinks_TypeDrinkId",
                table: "Ingredients");

            migrationBuilder.AlterColumn<int>(
                name: "TypeDrinkId",
                table: "Ingredients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_TypeDrinks_TypeDrinkId",
                table: "Ingredients",
                column: "TypeDrinkId",
                principalTable: "TypeDrinks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_TypeDrinks_TypeDrinkId",
                table: "Ingredients");

            migrationBuilder.AlterColumn<int>(
                name: "TypeDrinkId",
                table: "Ingredients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_TypeDrinks_TypeDrinkId",
                table: "Ingredients",
                column: "TypeDrinkId",
                principalTable: "TypeDrinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
