using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class addMoviebn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_ChatRecordEntityId",
                table: "MessageRecordEntities");

            migrationBuilder.AlterColumn<int>(
                name: "ChatRecordEntityId",
                table: "MessageRecordEntities",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_ChatRecordEntityId",
                table: "MessageRecordEntities",
                column: "ChatRecordEntityId",
                principalTable: "ChatRecordEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_ChatRecordEntityId",
                table: "MessageRecordEntities");

            migrationBuilder.AlterColumn<int>(
                name: "ChatRecordEntityId",
                table: "MessageRecordEntities",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_ChatRecordEntityId",
                table: "MessageRecordEntities",
                column: "ChatRecordEntityId",
                principalTable: "ChatRecordEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
