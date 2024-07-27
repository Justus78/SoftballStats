using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftballStats.Migrations
{
    /// <inheritdoc />
    public partial class UserTeamRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UserID",
                table: "Teams",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_UserID",
                table: "Teams",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_UserID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_UserID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Teams");
        }
    }
}
