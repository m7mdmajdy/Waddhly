using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waddhly.Migrations
{
    /// <inheritdoc />
    public partial class aa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_AspNetUsers_userId",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Portfolios",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolios_userId",
                table: "Portfolios",
                newName: "IX_Portfolios_userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_AspNetUsers_userid",
                table: "Portfolios",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_AspNetUsers_userid",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Portfolios",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolios_userid",
                table: "Portfolios",
                newName: "IX_Portfolios_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_AspNetUsers_userId",
                table: "Portfolios",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
