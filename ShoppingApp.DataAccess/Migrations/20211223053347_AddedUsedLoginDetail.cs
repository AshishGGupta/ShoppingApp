using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.Migrations
{
    public partial class AddedUsedLoginDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUsersDetails_Users_UserTokenUserId",
                table: "LoginUsersDetails");

            migrationBuilder.DropIndex(
                name: "IX_LoginUsersDetails_UserTokenUserId",
                table: "LoginUsersDetails");

            migrationBuilder.DropColumn(
                name: "UserTokenUserId",
                table: "LoginUsersDetails");

            migrationBuilder.AlterColumn<string>(
                name: "TokenUserId",
                table: "LoginUsersDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_LoginUsersDetails_TokenUserId",
                table: "LoginUsersDetails",
                column: "TokenUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUsersDetails_Users_TokenUserId",
                table: "LoginUsersDetails",
                column: "TokenUserId",
                principalTable: "Users",
                principalColumn: "TokenUserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUsersDetails_Users_TokenUserId",
                table: "LoginUsersDetails");

            migrationBuilder.DropIndex(
                name: "IX_LoginUsersDetails_TokenUserId",
                table: "LoginUsersDetails");

            migrationBuilder.AlterColumn<string>(
                name: "TokenUserId",
                table: "LoginUsersDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserTokenUserId",
                table: "LoginUsersDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginUsersDetails_UserTokenUserId",
                table: "LoginUsersDetails",
                column: "UserTokenUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUsersDetails_Users_UserTokenUserId",
                table: "LoginUsersDetails",
                column: "UserTokenUserId",
                principalTable: "Users",
                principalColumn: "TokenUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
