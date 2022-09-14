using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyFriend.Migrations
{
    public partial class DataValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topic",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Question",
                maxLength: 240,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Answer",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topic",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Question",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 240);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Answer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 320);
        }
    }
}
