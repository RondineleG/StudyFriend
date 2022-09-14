using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyFriend.Migrations
{
    public partial class UserTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Topic",
                newName: "TopicID");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Topic",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_UserId",
                table: "Topic",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_AspNetUsers_UserId",
                table: "Topic",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topic_AspNetUsers_UserId",
                table: "Topic");

            migrationBuilder.DropIndex(
                name: "IX_Topic_UserId",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Topic");

            migrationBuilder.RenameColumn(
                name: "TopicID",
                table: "Topic",
                newName: "ID");
        }
    }
}
