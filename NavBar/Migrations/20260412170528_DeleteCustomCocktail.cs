using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NavBar.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCustomCocktail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_Users_UserId",
                table: "Cocktails");

            migrationBuilder.DropIndex(
                name: "IX_Cocktails_UserId",
                table: "Cocktails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cocktails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cocktails",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cocktails_UserId",
                table: "Cocktails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_Users_UserId",
                table: "Cocktails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
