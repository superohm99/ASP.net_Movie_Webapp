using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class retime6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRecordEntities_AspNetUsers_AppUserId",
                table: "ChatRecordEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatEntityId",
                table: "ChatRecordEntities");

            migrationBuilder.RenameColumn(
                name: "ChatEntityId",
                table: "ChatRecordEntities",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRecordEntities_ChatEntityId",
                table: "ChatRecordEntities",
                newName: "IX_ChatRecordEntities_ChatId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "ChatRecordEntities",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRecordEntities_AspNetUsers_AppUserId",
                table: "ChatRecordEntities",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatId",
                table: "ChatRecordEntities",
                column: "ChatId",
                principalTable: "ChatEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRecordEntities_AspNetUsers_AppUserId",
                table: "ChatRecordEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatId",
                table: "ChatRecordEntities");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "ChatRecordEntities",
                newName: "ChatEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRecordEntities_ChatId",
                table: "ChatRecordEntities",
                newName: "IX_ChatRecordEntities_ChatEntityId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "ChatRecordEntities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRecordEntities_AspNetUsers_AppUserId",
                table: "ChatRecordEntities",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRecordEntities_ChatEntities_ChatEntityId",
                table: "ChatRecordEntities",
                column: "ChatEntityId",
                principalTable: "ChatEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
