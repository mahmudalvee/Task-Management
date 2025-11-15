using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AssignedToUserId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "TTasks");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TeamId",
                table: "TTasks",
                newName: "IX_TTasks_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssignedToUserId",
                table: "TTasks",
                newName: "IX_TTasks_AssignedToUserId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TTasks",
                table: "TTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TTasks_Teams_TeamId",
                table: "TTasks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TTasks_Users_AssignedToUserId",
                table: "TTasks",
                column: "AssignedToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TTasks_Teams_TeamId",
                table: "TTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TTasks_Users_AssignedToUserId",
                table: "TTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TTasks",
                table: "TTasks");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "TTasks",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_TTasks_TeamId",
                table: "Tasks",
                newName: "IX_Tasks_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TTasks_AssignedToUserId",
                table: "Tasks",
                newName: "IX_Tasks_AssignedToUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AssignedToUserId",
                table: "Tasks",
                column: "AssignedToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
