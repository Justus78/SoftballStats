using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftballStats.Migrations
{
    /// <inheritdoc />
    public partial class FIdSwap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatsID",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "Stats",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Stats");

            migrationBuilder.AddColumn<int>(
                name: "StatsID",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
