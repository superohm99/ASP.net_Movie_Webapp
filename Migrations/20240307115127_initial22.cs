using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class initial22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntities_CinemaEntities_CinemaEntityId",
                table: "MovieEntities");

            migrationBuilder.RenameColumn(
                name: "CinemaEntityId",
                table: "MovieEntities",
                newName: "CinemaId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieEntities_CinemaEntityId",
                table: "MovieEntities",
                newName: "IX_MovieEntities_CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntities_CinemaEntities_CinemaId",
                table: "MovieEntities",
                column: "CinemaId",
                principalTable: "CinemaEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntities_CinemaEntities_CinemaId",
                table: "MovieEntities");

            migrationBuilder.RenameColumn(
                name: "CinemaId",
                table: "MovieEntities",
                newName: "CinemaEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieEntities_CinemaId",
                table: "MovieEntities",
                newName: "IX_MovieEntities_CinemaEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntities_CinemaEntities_CinemaEntityId",
                table: "MovieEntities",
                column: "CinemaEntityId",
                principalTable: "CinemaEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
