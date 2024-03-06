using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntities_ChatEntities_ChatEntityId",
                table: "MovieEntities");

            migrationBuilder.DropIndex(
                name: "IX_MovieEntities_ChatEntityId",
                table: "MovieEntities");

            migrationBuilder.DropColumn(
                name: "ChatEntityId",
                table: "MovieEntities");

            migrationBuilder.AddColumn<int>(
                name: "MovieEntityId",
                table: "ChatEntities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntities_MovieEntityId",
                table: "ChatEntities",
                column: "MovieEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntities_MovieEntities_MovieEntityId",
                table: "ChatEntities",
                column: "MovieEntityId",
                principalTable: "MovieEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntities_MovieEntities_MovieEntityId",
                table: "ChatEntities");

            migrationBuilder.DropIndex(
                name: "IX_ChatEntities_MovieEntityId",
                table: "ChatEntities");

            migrationBuilder.DropColumn(
                name: "MovieEntityId",
                table: "ChatEntities");

            migrationBuilder.AddColumn<int>(
                name: "ChatEntityId",
                table: "MovieEntities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieEntities_ChatEntityId",
                table: "MovieEntities",
                column: "ChatEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntities_ChatEntities_ChatEntityId",
                table: "MovieEntities",
                column: "ChatEntityId",
                principalTable: "ChatEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
