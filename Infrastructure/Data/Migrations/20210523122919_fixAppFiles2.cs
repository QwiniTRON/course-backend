using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class fixAppFiles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AppFiles_UserPhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserPhotoId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AppFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppFiles_UserId",
                table: "AppFiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFiles_AspNetUsers_UserId",
                table: "AppFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFiles_AspNetUsers_UserId",
                table: "AppFiles");

            migrationBuilder.DropIndex(
                name: "IX_AppFiles_UserId",
                table: "AppFiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppFiles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserPhotoId",
                table: "AspNetUsers",
                column: "UserPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AppFiles_UserPhotoId",
                table: "AspNetUsers",
                column: "UserPhotoId",
                principalTable: "AppFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
