using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByIdAddedToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Games",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_CreatedById",
                table: "Games",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_CreatedById",
                table: "Games",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_CreatedById",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_CreatedById",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Games");
        }
    }
}
