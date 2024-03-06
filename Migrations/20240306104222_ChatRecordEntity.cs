using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class ChatRecordEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRecordEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreateChatEntityId = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRecordEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRecordEntity_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRecordEntity_CreateChatEntities_CreateChatEntityId",
                        column: x => x.CreateChatEntityId,
                        principalTable: "CreateChatEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRecordEntity_AppUserId",
                table: "ChatRecordEntity",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRecordEntity_CreateChatEntityId",
                table: "ChatRecordEntity",
                column: "CreateChatEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRecordEntity");
        }
    }
}
