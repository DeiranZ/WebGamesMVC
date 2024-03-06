using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GameImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Games");
        }
    }
}
