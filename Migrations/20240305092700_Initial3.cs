using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntities_AspNetUsers_AppUserId",
                table: "ChatEntities");

            migrationBuilder.DropIndex(
                name: "IX_ChatEntities_AppUserId",
                table: "ChatEntities");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ChatEntities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ChatEntities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntities_AppUserId",
                table: "ChatEntities",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntities_AspNetUsers_AppUserId",
                table: "ChatEntities",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
