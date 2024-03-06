using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class Initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CreateChatEntities_CreateChatEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreateChatEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateChatEntityId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUsersId",
                table: "CreateChatEntities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreateChatEntities_AppUsersId",
                table: "CreateChatEntities",
                column: "AppUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreateChatEntities_AspNetUsers_AppUsersId",
                table: "CreateChatEntities",
                column: "AppUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateChatEntities_AspNetUsers_AppUsersId",
                table: "CreateChatEntities");

            migrationBuilder.DropIndex(
                name: "IX_CreateChatEntities_AppUsersId",
                table: "CreateChatEntities");

            migrationBuilder.DropColumn(
                name: "AppUsersId",
                table: "CreateChatEntities");

            migrationBuilder.AddColumn<int>(
                name: "CreateChatEntityId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreateChatEntityId",
                table: "AspNetUsers",
                column: "CreateChatEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CreateChatEntities_CreateChatEntityId",
                table: "AspNetUsers",
                column: "CreateChatEntityId",
                principalTable: "CreateChatEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
