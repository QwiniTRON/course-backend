using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class FixPracticeOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracticeOrders_AspNetUsers_TeacherId",
                table: "PracticeOrders");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "PracticeOrders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PracticeOrders_AspNetUsers_TeacherId",
                table: "PracticeOrders",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracticeOrders_AspNetUsers_TeacherId",
                table: "PracticeOrders");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "PracticeOrders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PracticeOrders_AspNetUsers_TeacherId",
                table: "PracticeOrders",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
