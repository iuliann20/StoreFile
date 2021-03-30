using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreFile.DAL.Migrations
{
    public partial class removeUserIdForeighKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokens_Users_StoredFileUserUserId",
                table: "AccessTokens");

            migrationBuilder.DropIndex(
                name: "IX_AccessTokens_StoredFileUserUserId",
                table: "AccessTokens");

            migrationBuilder.DropColumn(
                name: "StoredFileUserUserId",
                table: "AccessTokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccessTokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoredFileUserUserId",
                table: "AccessTokens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AccessTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokens_StoredFileUserUserId",
                table: "AccessTokens",
                column: "StoredFileUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokens_Users_StoredFileUserUserId",
                table: "AccessTokens",
                column: "StoredFileUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
