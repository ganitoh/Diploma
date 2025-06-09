using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_user_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "refreshtoken",
                newSchema: "identity");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "identity",
                table: "refreshtoken",
                newName: "IX_refreshtoken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshtoken",
                schema: "identity",
                table: "refreshtoken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshtoken_user_UserId",
                schema: "identity",
                table: "refreshtoken",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refreshtoken_user_UserId",
                schema: "identity",
                table: "refreshtoken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshtoken",
                schema: "identity",
                table: "refreshtoken");

            migrationBuilder.RenameTable(
                name: "refreshtoken",
                schema: "identity",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_refreshtoken_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_user_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
