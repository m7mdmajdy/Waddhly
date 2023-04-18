using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waddhly.Migrations
{
    /// <inheritdoc />
    public partial class updateSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "exactCost",
                table: "Sessions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isPaid",
                table: "Sessions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "exactCost",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "isPaid",
                table: "Sessions");
        }
    }
}
