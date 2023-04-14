using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waddhly.Migrations
{
    /// <inheritdoc />
    public partial class j : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_AspNetUsers_userId",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Certificates",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_userId",
                table: "Certificates",
                newName: "IX_Certificates_userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_AspNetUsers_userid",
                table: "Certificates",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_AspNetUsers_userid",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Certificates",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_userid",
                table: "Certificates",
                newName: "IX_Certificates_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_AspNetUsers_userId",
                table: "Certificates",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
