using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NavBar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldInTypeDrink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "TypeDrinks",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TypeDrinks",
                newName: "Type");
        }
    }
}
