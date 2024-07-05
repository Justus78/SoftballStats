using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftballStats.Migrations
{
    /// <inheritdoc />
    public partial class addedGameDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GameDate",
                table: "Stats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Opponent",
                table: "Stats",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameDate",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Opponent",
                table: "Stats");
        }
    }
}
