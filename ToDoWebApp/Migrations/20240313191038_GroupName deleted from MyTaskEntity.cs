using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoWebApp.Migrations
{
    public partial class GroupNamedeletedfromMyTaskEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
