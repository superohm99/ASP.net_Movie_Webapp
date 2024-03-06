using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatEntityId",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ChatEntityId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
