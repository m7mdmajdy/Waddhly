using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waddhly.Migrations
{
    /// <inheritdoc />
    public partial class ss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_userId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_postID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_userId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Posts",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_userId",
                table: "Posts",
                newName: "IX_Posts_userid");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Comments",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "postID",
                table: "Comments",
                newName: "postid");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_userId",
                table: "Comments",
                newName: "IX_Comments_userid");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_postID",
                table: "Comments",
                newName: "IX_Comments_postid");

            migrationBuilder.AlterColumn<int>(
                name: "postid",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_userid",
                table: "Comments",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_postid",
                table: "Comments",
                column: "postid",
                principalTable: "Posts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_userid",
                table: "Posts",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_userid",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_postid",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_userid",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Posts",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_userid",
                table: "Posts",
                newName: "IX_Posts_userId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Comments",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "postid",
                table: "Comments",
                newName: "postID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_userid",
                table: "Comments",
                newName: "IX_Comments_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_postid",
                table: "Comments",
                newName: "IX_Comments_postID");

            migrationBuilder.AlterColumn<int>(
                name: "postID",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_userId",
                table: "Comments",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_postID",
                table: "Comments",
                column: "postID",
                principalTable: "Posts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_userId",
                table: "Posts",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
