using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class retime2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRecordEntities_CreateChatEntities_CreateChatEntityId",
                table: "ChatRecordEntities");

            migrationBuilder.DropTable(
                name: "CreateChatEntities");

            migrationBuilder.RenameColumn(
                name: "CreateChatEntityId",
                table: "ChatRecordEntities",
                newName: "ChatEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRecordEntities_CreateChatEntityId",
                table: "ChatRecordEntities",
                newName: "IX_ChatRecordEntities_ChatEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatEntityId",
                table: "ChatRecordEntities",
                column: "ChatEntityId",
                principalTable: "ChatEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatEntityId",
                table: "ChatRecordEntities");

            migrationBuilder.RenameColumn(
                name: "ChatEntityId",
                table: "ChatRecordEntities",
                newName: "CreateChatEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRecordEntities_ChatEntityId",
                table: "ChatRecordEntities",
                newName: "IX_ChatRecordEntities_CreateChatEntityId");

            migrationBuilder.CreateTable(
                name: "CreateChatEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUsersId = table.Column<string>(type: "text", nullable: true),
                    ChatEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateChatEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreateChatEntities_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreateChatEntities_ChatEntities_ChatEntityId",
                        column: x => x.ChatEntityId,
                        principalTable: "ChatEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreateChatEntities_AppUsersId",
                table: "CreateChatEntities",
                column: "AppUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_CreateChatEntities_ChatEntityId",
                table: "CreateChatEntities",
                column: "ChatEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRecordEntities_CreateChatEntities_CreateChatEntityId",
                table: "ChatRecordEntities",
                column: "CreateChatEntityId",
                principalTable: "CreateChatEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
