using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waddhly.Migrations
{
    /// <inheritdoc />
    public partial class addIsDoneProposalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Proposals",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Proposals");
        }
    }
}
