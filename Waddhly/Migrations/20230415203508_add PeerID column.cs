using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waddhly.Migrations
{
    /// <inheritdoc />
    public partial class addPeerIDcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PeerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeerId",
                table: "AspNetUsers");
        }
    }
}
