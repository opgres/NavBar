using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NavBar.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLogicPricePerVolume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvgBuyPrice",
                table: "Ingredients",
                newName: "PricePerVolume");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerVolume",
                table: "Ingredients",
                newName: "AvgBuyPrice");
        }
    }
}
