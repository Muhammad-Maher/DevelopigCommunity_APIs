using Microsoft.EntityFrameworkCore.Migrations;

namespace DevelopigCommunityService.Migrations
{
    public partial class changeinstructor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganiztionTypeId",
                table: "Organizations",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganiztionTypeId",
                table: "Organizations");
        }
    }
}
