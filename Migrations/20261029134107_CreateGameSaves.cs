using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPG.Migrations
{
    /// <inheritdoc />
    public partial class CreateGameSaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaveName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentMapId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsGameWon = table.Column<bool>(type: "bit", nullable: false),
                    SaveData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSaves", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSaves");
        }
    }
}
