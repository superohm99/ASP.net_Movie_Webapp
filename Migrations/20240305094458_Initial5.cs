using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateChatEntityId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CreateChatEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateChatEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreateChatEntities_ChatEntities_ChatEntityId",
                        column: x => x.ChatEntityId,
                        principalTable: "ChatEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreateChatEntityId",
                table: "AspNetUsers",
                column: "CreateChatEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CreateChatEntities_ChatEntityId",
                table: "CreateChatEntities",
                column: "ChatEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CreateChatEntities_CreateChatEntityId",
                table: "AspNetUsers",
                column: "CreateChatEntityId",
                principalTable: "CreateChatEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CreateChatEntities_CreateChatEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CreateChatEntities");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreateChatEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateChatEntityId",
                table: "AspNetUsers");
        }
    }
}
