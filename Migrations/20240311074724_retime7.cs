using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class retime7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatId",
                table: "ChatRecordEntities");

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "ChatRecordEntities",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatId",
                table: "ChatRecordEntities",
                column: "ChatId",
                principalTable: "ChatEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatId",
                table: "ChatRecordEntities");

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "ChatRecordEntities",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatId",
                table: "ChatRecordEntities",
                column: "ChatId",
                principalTable: "ChatEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
