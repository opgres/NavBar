using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NavBar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForNewRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ro",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Ingredients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ro",
                table: "Ingredients",
                type: "text",
                nullable: true);
        }
    }
}
