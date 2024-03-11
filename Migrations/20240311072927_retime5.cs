using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class retime5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_chatRecordEntityId",
                table: "MessageRecordEntities");

            migrationBuilder.RenameColumn(
                name: "chatRecordEntityId",
                table: "MessageRecordEntities",
                newName: "ChatRecordEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageRecordEntities_chatRecordEntityId",
                table: "MessageRecordEntities",
                newName: "IX_MessageRecordEntities_ChatRecordEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_ChatRecordEntityId",
                table: "MessageRecordEntities",
                column: "ChatRecordEntityId",
                principalTable: "ChatRecordEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_ChatRecordEntityId",
                table: "MessageRecordEntities");

            migrationBuilder.RenameColumn(
                name: "ChatRecordEntityId",
                table: "MessageRecordEntities",
                newName: "chatRecordEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageRecordEntities_ChatRecordEntityId",
                table: "MessageRecordEntities",
                newName: "IX_MessageRecordEntities_chatRecordEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_chatRecordEntityId",
                table: "MessageRecordEntities",
                column: "chatRecordEntityId",
                principalTable: "ChatRecordEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
