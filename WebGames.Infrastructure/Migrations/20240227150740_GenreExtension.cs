using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GenreExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Genres",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Genres",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EncodedName",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_CreatedById",
                table: "Genres",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_AspNetUsers_CreatedById",
                table: "Genres",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_AspNetUsers_CreatedById",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_CreatedById",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "EncodedName",
                table: "Genres");
        }
    }
}
