using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftballStats.Migrations
{
    /// <inheritdoc />
    public partial class TeamPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamID",
                table: "Players",
                column: "TeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamID",
                table: "Players",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "TeamID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamID",
                table: "Players");
        }
    }
}
