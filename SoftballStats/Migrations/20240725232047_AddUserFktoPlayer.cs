using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftballStats.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFktoPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Players",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserID",
                table: "Players",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_AspNetUsers_UserID",
                table: "Players",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_AspNetUsers_UserID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_UserID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Players");
        }
    }
}
