using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waddhly.Migrations
{
    /// <inheritdoc />
    public partial class addRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user1Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    user2Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rooms_AspNetUsers_user1Id",
                        column: x => x.user1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rooms_AspNetUsers_user2Id",
                        column: x => x.user2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_user1Id",
                table: "Rooms",
                column: "user1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_user2Id",
                table: "Rooms",
                column: "user2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
