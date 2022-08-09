using Microsoft.EntityFrameworkCore.Migrations;

namespace MEMOJET.Migrations
{
    public partial class newmigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "UploadedDocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UploadedDocs_CommentId",
                table: "UploadedDocs",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedDocs_Comments_CommentId",
                table: "UploadedDocs",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadedDocs_Comments_CommentId",
                table: "UploadedDocs");

            migrationBuilder.DropIndex(
                name: "IX_UploadedDocs_CommentId",
                table: "UploadedDocs");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "UploadedDocs");
        }
    }
}
