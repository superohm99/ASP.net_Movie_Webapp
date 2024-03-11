using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class retime4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "chatRecordEntityId",
                table: "MessageRecordEntities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecordEntities_chatRecordEntityId",
                table: "MessageRecordEntities",
                column: "chatRecordEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_chatRecordEntityId",
                table: "MessageRecordEntities",
                column: "chatRecordEntityId",
                principalTable: "ChatRecordEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecordEntities_ChatRecordEntities_chatRecordEntityId",
                table: "MessageRecordEntities");

            migrationBuilder.DropIndex(
                name: "IX_MessageRecordEntities_chatRecordEntityId",
                table: "MessageRecordEntities");

            migrationBuilder.DropColumn(
                name: "chatRecordEntityId",
                table: "MessageRecordEntities");
        }
    }
}
