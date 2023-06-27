using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Database.Migrations
{
    public partial class adduserIdentifierId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentifierId",
                table: "User",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdentifierId",
                table: "User",
                column: "IdentifierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_IdentifierId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IdentifierId",
                table: "User");
        }
    }
}
