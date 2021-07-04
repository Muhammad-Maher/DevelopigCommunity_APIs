using Microsoft.EntityFrameworkCore.Migrations;

namespace DevelopigCommunityService.Migrations
{
    public partial class addfieldstoprojecttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVideos_ProjectId",
                table: "ProjectVideos",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectVideos_Projects_ProjectId",
                table: "ProjectVideos",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectVideos_Projects_ProjectId",
                table: "ProjectVideos");

            migrationBuilder.DropIndex(
                name: "IX_ProjectVideos_ProjectId",
                table: "ProjectVideos");

            migrationBuilder.DropColumn(
                name: "CodeUrl",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");
        }
    }
}
