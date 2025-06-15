using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.EnsureSchema(
                name: "chat");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "message",
                newSchema: "chat");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "chat",
                newSchema: "chat");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatId",
                schema: "chat",
                table: "message",
                newName: "IX_message_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_message",
                schema: "chat",
                table: "message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chat",
                schema: "chat",
                table: "chat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_message_chat_ChatId",
                schema: "chat",
                table: "message",
                column: "ChatId",
                principalSchema: "chat",
                principalTable: "chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_chat_ChatId",
                schema: "chat",
                table: "message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_message",
                schema: "chat",
                table: "message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chat",
                schema: "chat",
                table: "chat");

            migrationBuilder.RenameTable(
                name: "message",
                schema: "chat",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "chat",
                schema: "chat",
                newName: "Chats");

            migrationBuilder.RenameIndex(
                name: "IX_message_ChatId",
                table: "Messages",
                newName: "IX_Messages_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
