using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddUserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "default.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
