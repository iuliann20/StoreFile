using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreFile.DAL.Migrations
{
    public partial class AddForeighKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AccessTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokens_UserId",
                table: "AccessTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokens_Users_UserId",
                table: "AccessTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokens_Users_UserId",
                table: "AccessTokens");

            migrationBuilder.DropIndex(
                name: "IX_AccessTokens_UserId",
                table: "AccessTokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccessTokens");
        }
    }
}
