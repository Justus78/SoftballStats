using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftballStats.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    StatsID = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    StatsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GamesPlayed = table.Column<int>(type: "int", nullable: true),
                    AtBats = table.Column<int>(type: "int", nullable: true),
                    Hits = table.Column<int>(type: "int", nullable: true),
                    Runs = table.Column<int>(type: "int", nullable: true),
                    RBIs = table.Column<int>(type: "int", nullable: true),
                    Walks = table.Column<int>(type: "int", nullable: true),
                    Strikeouts = table.Column<int>(type: "int", nullable: true),
                    StolenBases = table.Column<int>(type: "int", nullable: true),
                    Errors = table.Column<int>(type: "int", nullable: true),
                    Singles = table.Column<int>(type: "int", nullable: true),
                    Doubles = table.Column<int>(type: "int", nullable: true),
                    Triples = table.Column<int>(type: "int", nullable: true),
                    HomeRuns = table.Column<int>(type: "int", nullable: true),
                    HitByPitch = table.Column<int>(type: "int", nullable: true),
                    SacrificeFly = table.Column<int>(type: "int", nullable: true),
                    SacrificeBunt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.StatsID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
